namespace Chemicals.ConsoleTesterApp
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    using Chemicals.Data.SQLServer;
    using Chemicals.Data.SQLServer.Migrations;
    using Chemicals.ExcelImporter;
    using Chemicals.Models;

    public class Startup
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChemicalsDbContext, Configuration>());

        var db = new ChemicalsDbContext();
       //var manufacturer = new Manufacturer() { Name = "PSI", NumberOfFactories = 5, Address = "Aleksandar Malinov 102" };
       //
       //db.Manufacturers.AddOrUpdate(manufacturer);
          // db.SaveChanges();

            ExelImporter k = new ExelImporter();
            ICollection<Sale> sales = k.ImportModelsDataFromDirectory<Sale>(@".\tests");

            ICollection<Sale> salesZip = k.ImportModelsDataFromZipFile<Sale>(@".\test.zip");

            var i = 1;
            foreach (Sale sale in sales)
            {
                db.Sales.Add(sale);
                
                if (i % 100 == 0)
                {
                    db.SaveChanges();
                    db.Dispose();
                    db = new ChemicalsDbContext();
                    i = 0;
                }

                i++;
            }
            db.SaveChanges();
        }
    }
}
