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
    using XmlData;
    using Export.JSON;
    using DataImport;

    public class Startup
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChemicalsDbContext, Data.SQLServer.Migrations.Configuration>());


            //ImportDataFromMongo();

            //ImportTradersFromXml("../../../Files/traders.xml");
            //ImportManufacturersFromXml("../../../Files/manufacturers.xml");

            //GenerateXmlReports();

            // TODO: test when sels are added
            //GenerateJsonReports();

            //GeneratePdfReports();

            ImportSalesFromExcel();


         
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

        }

        private static void ImportSalesFromExcel()
        {
            var db = new ChemicalsDbContext();

            IZipExtractor zipExtractor = new ZipExtractor();
            ExcelImporter<Sale> k = new ExcelImporter<Sale>(zipExtractor);
            ICollection<Sale> sales = k.ImportModelsDataFromZipFile("../../../Files/Files.zip");

            foreach (var item in sales)
            {
                System.Console.WriteLine(item.TraderId);
            }
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
        }

        private static void GeneratePdfReports()
        {
            var pdfReportsGenerator = new PdfReportGenerator();

            var dbContext = new ChemicalsDbContext();

            var deals = dbContext.Produces
                    .Select(d => new
                    {
                        d.Id,
                        d.Manufacturer.Name,
                        ProductName = d.Product.Name,
                        d.Manufacturer.Address,
                        d.Product.Formula
                    }).ToList();

            pdfReportsGenerator.GenerateReport(deals);
        }

        // TODO: add path to the method
        private static void GenerateJsonReports()
        {
            var dbContext = new ChemicalsDbContext();

            var listOfProducts = dbContext.Products.ToList();

            ExportSQLToJSON.ExportProducts(listOfProducts, "../../Reports/");
        }

        // TODO: add path to the method
        private static void GenerateXmlReports()
        {
            using (var db = new ChemicalsDbContext())
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

        private static void ImportTradersFromXml(string path)
        {
            var xmlImporter = new XmlDataImporter();

            var dbContext = new ChemicalsDbContext();

            var traders = xmlImporter.LoadTraders(path);

            foreach (var trader in traders)
            {
                dbContext.Traders.Add(trader);
            }

            dbContext.SaveChanges();
            dbContext.Dispose();
        }

        private static void ImportManufacturersFromXml(string path)
        {
            var xmlImporter = new XmlDataImporter();

            var dbContext = new ChemicalsDbContext();

            var manufacturers = xmlImporter.LoadManufecturers(path);

            foreach (var manufacturer in manufacturers)
            {
                dbContext.Manufacturers.Add(manufacturer);
            }

            dbContext.SaveChanges();
            dbContext.Dispose();
        }

        private static void ImportDataFromMongo()
        {
            var dbContext = new ChemicalsDbContext();

            var mongoProvider = new MongoProvider(
                System.Configuration.ConfigurationManager.ConnectionStrings["MolybdenumDb"].ConnectionString,
                System.Configuration.ConfigurationManager.ConnectionStrings["MolybdenumDb"].Name);

            var mongoDatabase = mongoProvider.GetDatabase();

            var mongoImporter = new MongoImporter();
            var products = mongoImporter.GetAllProducts(mongoDatabase, "Products");

            foreach (var product in products)
            {
                dbContext.Products.Add(product);
            }

            dbContext.SaveChanges();

        }

    }
}

