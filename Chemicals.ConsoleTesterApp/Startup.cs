namespace Chemicals.ConsoleTesterApp
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Data.SQLServer;
    using DataImport;
    using ExcelDataIE;
    using ExcelImporter.Contracts;
    using Export.JSON;
    using Models;
    using MongoData.MongoDb;
    using MySqlData;
    using MySqlData.Models;
    using Telerik.OpenAccess;
    using XmlData;
    using XmlReport;

    public class Startup
    {
        public static void Main()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChemicalsDbContext, Data.SQLServer.Migrations.Configuration>());

            //ImportDataFromMongo();

            //ImportTradersFromXml("../../../Files/traders.xml");
            //ImportManufacturersFromXml("../../../Files/manufacturers.xml");

            //GenerateXmlReports("../../../ManufacturerersReport.xml");

            // TODO: test when sels are added
            //GenerateJsonReports();

            //GeneratePdfReports();

            //ImportSalesFromExcel();

<<<<<<< HEAD
            ImportProducesFromExcel();


=======
>>>>>>> f21c78b5aeeeb299c2c84b093fbfe5b2fc28e707
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

        private static void MySqlDataTest()
        {
            using (var dbContext = new FluentModelContent())
            {
                Report newReport = new Report
                {
                    Name = "First Report",
                    Type = "First type",
                    Vendor = "Stamat",
                    PricePerUnit = "13",
                    Sold = "14",
                    TotalIncome = "515"
                };

                dbContext.Add(newReport);
                dbContext.SaveChanges();
            }
        }

        private static void ImportProducesFromExcel()
        {
            var db = new ChemicalsDbContext();

            IZipExtractor zipExtractor = new ZipExtractor();
            ExcelImporter<Produce> k = new ExcelImporter<Produce>(zipExtractor);
            ICollection<Produce> produces = k.ImportModelsDataFromZipFile("../../../Files/Produces.zip", "./tests1");

            foreach (var item in produces)
            {
                System.Console.WriteLine(item.ProducedDate);
            }
        }

        private static void ImportSalesFromExcel()
        {
            var db = new ChemicalsDbContext();

            IZipExtractor zipExtractor = new ZipExtractor();
            ExcelImporter<Sale> k = new ExcelImporter<Sale>(zipExtractor);
            ICollection<Sale> sales = k.ImportModelsDataFromZipFile("../../../Files/Sales.zip", "./tests2");

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

        private static void GenerateXmlReports(string pathToSave)
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

                foreach (var manufacturer in manufacturers)
                {
                    productsList.Add(new XmlProduct
                    {
                        Name = manufacturer.ProductName,
                        Amout = manufacturer.Amount,
                        Formula = manufacturer.Formula
                    });

                    var currentManufacturer = new XmlManufacturer
                    {
                        Name = manufacturer.ManufacturerName,
                        Products = productsList
                    };

                    manufacturersList.Add(currentManufacturer);
                }

                var report = new XmlReportModel
                {
                    Manufacturers = manufacturersList
                };

                var reportGenerator = new XmlReportGenerator();
                reportGenerator.ExportXmlReport(report, pathToSave);
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
