namespace Chemicals.ConsoleTesterApp
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Configuration;

    using Chemicals.Data.SQLServer;
    using Chemicals.Data.SQLServer.Migrations;
    using Chemicals.ExcelDataIE;
    using Chemicals.ExcelImporter;
    using Chemicals.ExcelImporter.Contracts;
    using Chemicals.Models;
    using Chemicals.MongoData.MongoDb;
    using XmlReport;

    public class Startup
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChemicalsDbContext, Data.SQLServer.Migrations.Configuration>());

            var db = new ChemicalsDbContext();
            var manufacturer = new Manufacturer() { Name = "PSI", NumberOfFactories = 5, Address = "Aleksandar Malinov 102" };

            db.Manufacturers.AddOrUpdate(manufacturer);
          //  db.SaveChanges();
            System.Console.WriteLine(db.Manufacturers.Count());
            //IZipExtractor zipExtractor = new ZipExtractor();
            //ExcelImporter<Sale> k = new ExcelImporter<Sale>(zipExtractor);
            //ICollection<Sale> sales = k.ImportModelsDataFromDirectory(@".\tests");

            //ExcelExporter<Sale> l = new ExcelExporter<Sale>();
            //l.ExportDataModelsCollectionToExcelFile(@".\", "Test2", "Test3", sales);

            //ICollection<Sale> salesZip = k.ImportModelsDataFromZipFile(@".\test.zip");

            //var i = 1;
            //foreach (Sale sale in sales)
            //{
            //    db.Sales.Add(sale);

            //    if (i % 100 == 0)
            //    {
            //        db.SaveChanges();
            //        db.Dispose();
            //        db = new ChemicalsDbContext();
            //        i = 0;
            //    }

            //    i++;
            //}

            //db.SaveChanges();

            //var mongoProvider = new MongoProvider(
            //    System.Configuration.ConfigurationManager.ConnectionStrings["MolybdenumDb"].ConnectionString,
            //    System.Configuration.ConfigurationManager.ConnectionStrings["MolybdenumDb"].Name);

            //var mongoDatabase = mongoProvider.GetDatabase();

            //var mongoImporter = new MongoImporter();
            //var products = mongoImporter.GetAllProducts(mongoDatabase, "Products");

            //foreach (var item in products)
            //{
            //    System.Console.WriteLine(item.Name);
            //}

            //// Generate XML report

            using (db = new ChemicalsDbContext())
            {
                var manufacturers = (from man in db.Manufacturers.Include("Name")
                                     join p in db.Produces on man.Id equals p.ManufacturerId
                                     join pr in db.Products.Include("Name").Include("Formula") on p.ProductId equals pr.Id
                                     select new
                                     {
                                         ManufacturerName = man.Name,
                                         ProductName = pr.Name,
                                         Amount = p.Amount,
                                         Formula = pr.Formula
                                     }).ToList();


                var manufacturersList = new List<XmlManufacturer>();
                var productsList = new List<XmlProduct>();

                foreach (var man in manufacturers)
                {
                    productsList.Add(new XmlProduct
                    {
                        Name = man.ProductName,
                        Amout = man.Amount,
                        Formula = man.Formula
                    });

                    var currentManufacturer = new XmlManufacturer
                    {
                        Name = man.ManufacturerName,
                        Products = productsList
                    };

                    manufacturersList.Add(currentManufacturer);
                }

                var currentReport = new XmlReportModel
                {
                    Manufacturers = manufacturersList
                };

                var reportGenerator = new XmlReportGenerator();
                reportGenerator.ExportXmlReport(currentReport);

            }


        }
    }
}
