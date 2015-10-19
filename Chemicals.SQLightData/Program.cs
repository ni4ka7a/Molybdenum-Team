namespace Chemicals.SQLightData
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
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
                // Console.WriteLine(db.RadioactiveProducts.Count());
            }
        }
    }
}
