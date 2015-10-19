namespace Chemicals.ConsoleTesterApp
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Data.SQLServer;
    using DataImport;
    using ExcelDataIE;
    using ExcelImporter.Contracts;
    using Models;
    using MongoData.MongoDb;
    using MySqlData;
    using MySqlData.Models;
    using Telerik.OpenAccess;
    using XmlData;
    using XmlReport;
    using ReportingServices;
    using SQLightData;

    public class Startup
    {
        private const string TradersXmlFileName = "../../../Files/traders.xml";
        private const string ManufacturersXmlFileName = "../../../Files/manufacturers.xml";
        private const string XmlReportSavePath = "../../report.xml";

        public static void Main()
        {
            // System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChemicalsDbContext, Data.SQLServer.Migrations.Configuration>());

            while (true)
            {
                Console.WriteLine("Enter command:");

                Console.WriteLine("1.Load data from mongoDB");
                Console.WriteLine("2.Load data from xml");
                Console.WriteLine("3.Load data from Excel");
                Console.WriteLine("4.Get all products from Sql Server");
                Console.WriteLine("5.Generate PDF Report");
                Console.WriteLine("6.Generate XML Report");
                Console.WriteLine("7.Generate JSON Reports");
                Console.WriteLine("8.Load JSON Reports to MySql");
                Console.WriteLine("9.Generate Excel table from Mysql Data");
                var command = int.Parse(Console.ReadLine());

                switch (command)
                {
                    case 1:
                        ImportDataFromMongo();
                        break;
                    case 2:
                        ImportManufacturersFromXml(ManufacturersXmlFileName);
                        ImportTradersFromXml(TradersXmlFileName);
                        break;
                    case 3:
                        ImportProducesFromExcel();
                        ImportSalesFromExcel();
                        break;
                    case 4:
                        GetAllProducts();
                        break;
                    case 5:
                        GeneratePdfReports();
                        break;
                    case 6:
                        GenerateXmlReports(XmlReportSavePath);
                        break;
                    case 7:
                        GenerateJsonReports();
                        break;
                    case 8:
                        ExportDataFromJsonReportsToMySql();
                        break;
                    case 9:
                        ExportReportsToExcel();
                        break;
                    default:
                        break;
                }

                Console.WriteLine("Press any key to continue..");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private static void GetAllProducts()
        {
            using (var dbContext = new ChemicalsDbContext())
            {
                var products = dbContext.Products.ToList();

                foreach (var product in products)
                {
                    Console.WriteLine(
                        "{0}, {1}, {2}, {3}",
                        product.Id, product.Name, product.Formula, product.PricePerUnit);
                }
            }
        }

        private static void ExportReportsToExcel()
        {
            using (var dbContext = new FluentModelContent())
            {
                var reports = dbContext.Reports.ToList();

                var excelExporter = new ExcelExporter<Report>();
                excelExporter.ExportDataModelsCollectionToExcelFile("../../", "report", "someName", reports);
            }

            Console.WriteLine("The Excel table was successfully writen.");
        }

        private static void ExportDataFromJsonReportsToMySql()
        {
            using (var dbContext = new FluentModelContent())
            {
                var reports = ExportSQLToJSON.ImportProductsInfo("../../../Reports/");

                foreach (var item in reports)
                {
                    dbContext.Add(item);
                }

                dbContext.SaveChanges();
            }

            Console.WriteLine("The reports was successfully loaded in MySql.");
        }

        private static void ImportProducesFromExcel()
        {
            var db = new ChemicalsDbContext();

            IZipExtractor zipExtractor = new ZipExtractor();
            ExcelImporter<Produce> k = new ExcelImporter<Produce>(zipExtractor);
            ICollection<Produce> produces = k.ImportModelsDataFromZipFile("../../../Files/Produces.zip", "./tests1");

            foreach (var item in produces)
            {
                db.Produces.Add(item);
            }

            db.SaveChanges();
        }

        private static void ImportSalesFromExcel()
        {
            var db = new ChemicalsDbContext();

            IZipExtractor zipExtractor = new ZipExtractor();
            ExcelImporter<Sale> k = new ExcelImporter<Sale>(zipExtractor);
            ICollection<Sale> sales = k.ImportModelsDataFromZipFile("../../../Files/Sales.zip", "./tests2");

            foreach (var item in sales)
            {
                db.Sales.Add(item);
            }

            db.SaveChanges();

            Console.WriteLine("The data was successfully imported to SQL Server.");
        }

        private static void GeneratePdfReports()
        {
            var pdfReportsGenerator = new PdfReportGenerator();

            var dbContext = new ChemicalsDbContext();

            var deals = dbContext.Sales
                    .Select(d => new
                    {
                        ProductName = d.Product.Name,
                        Quantity = (d.Quantity + " " + d.Product.Measure.MeasureName).ToString(),
                        PricePerUnit = (d.Product.PricePerUnit).ToString(),
                        d.Product.Formula,
                        d.Trader.Address,
                        Total = (d.Quantity * d.Product.PricePerUnit).ToString()
                    }).ToList();

            pdfReportsGenerator.GenerateReport(deals);

            Console.WriteLine("The report was successfully generated.");
        }

        private static void GenerateJsonReports()
        {
            var dbContext = new ChemicalsDbContext();

            var listOfProducts = dbContext.Products.ToList();

            ExportSQLToJSON.ExportProducts(listOfProducts, "../../../Reports/");

            Console.WriteLine("The reports was successfully generated.");
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

            Console.WriteLine("The report was successfully generated.");
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
            ExportXmlTradersToMongo(traders);
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
            ExportXmlManufacturersToMongo(manufacturers);

            Console.WriteLine("The data was successfully imported to SQL Server and to MongoDB.");
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

            Console.WriteLine("The data was successfully imported to the SQL Server.");
        }

        // TODO: ImportManufacturers in mongo
        private static void ExportXmlManufacturersToMongo(ICollection<Manufacturer> manufacturers)
        {
            var mongoProvider = new MongoProvider(
                System.Configuration.ConfigurationManager.ConnectionStrings["MolybdenumDb"].ConnectionString,
                System.Configuration.ConfigurationManager.ConnectionStrings["MolybdenumDb"].Name);

            var mongoDatabase = mongoProvider.GetDatabase();

            var mongoExporter = new MongoExporter();
            mongoExporter.ExportManufacturers(mongoDatabase, manufacturers);
        }

        private static void ExportXmlTradersToMongo(ICollection<Trader> traders)
        {
            var mongoProvider = new MongoProvider(
               System.Configuration.ConfigurationManager.ConnectionStrings["MolybdenumDb"].ConnectionString,
               System.Configuration.ConfigurationManager.ConnectionStrings["MolybdenumDb"].Name);

            var mongoDatabase = mongoProvider.GetDatabase();

            var mongoExporter = new MongoExporter();
            mongoExporter.ExportTraders(mongoDatabase, traders);
        }
    }
}
