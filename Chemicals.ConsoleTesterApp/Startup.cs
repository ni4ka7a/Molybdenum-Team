namespace Chemicals.ConsoleTesterApp
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    using Chemicals.Data.SQLServer;
    using Chemicals.Data.SQLServer.Migrations;
    using Chemicals.Models;

    public class Startup
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChemicalsDbContext, Configuration>());

          var db = new ChemicalsDbContext();
        
         var manufacturer = new Manufacturer() { Name = "Alkaloid", NumberOfFactories = 5, Address = "Aleksandar Malinov 102" };
        
         db.Manufacturers.AddOrUpdate(manufacturer);
           db.SaveChanges();
        }
    }
}
