namespace Chemicals.ConsoleTesterApp
{
    using System.Data.Entity;

    using Chemicals.Data;
    using Chemicals.Data.Migrations;
    using Chemicals.Models;

    public class Startup
    {
        public static void Main()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ChemicalsDbContext, Configuration>());

            var db = new ChemicalsDbContext();


            var manufacturer = new Manufacturer() { Name = "Alkaloid", NumberOfFactories = 5, Address = "Aleksandar Malinov 102" };

            db.Manufacturers.Add(manufacturer);
            db.SaveChanges();
        }
    }
}
