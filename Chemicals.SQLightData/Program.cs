namespace Chemicals.SQLightData
{
    using System;
    using  System.Collections.Generic;
    using  Chemicals.Models;

    public class Program
    {
        public static void Main()
        {
            using (var db = new RadioactivityDbContext())
            {
                RadioactiveProduct product = new RadioactiveProduct();
                product.Radioactivity = 5;
                product.ProductName = "jfksdjgdlk";
                db.RadioactiveProducts.Add(product);

                db.SaveChanges();

                foreach (var radioactiveProduct in db.RadioactiveProducts)
                {
                    Console.WriteLine(radioactiveProduct.ProductName);
                }
            }
        }

        public static void GetProducts(ICollection<Product> products)
        {
            using (var db = new RadioactivityDbContext())
            {


                foreach (var prod in products)
                {
                    var radProduct = new RadioactiveProduct();
                    radProduct.Id = prod.Id;
                    radProduct.ProductName = prod.Name;
                    var random = new Random();
                    radProduct.Radioactivity = random.Next(1, 10);
                }
            }
        }
    }
}
