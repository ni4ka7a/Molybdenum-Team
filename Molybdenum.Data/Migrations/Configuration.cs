namespace Chemicals.Data.SQLServer.Migrations
{
    using System.Data.Entity.Migrations;

    using Chemicals.Models;

    public sealed class Configuration : DbMigrationsConfiguration<ChemicalsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "Chemicals.Data.ChemicalsDbContext";
        }

        protected override void Seed(ChemicalsDbContext context)
        {
            ////context.Traders.AddOrUpdate(
            ////    a => a.Name,
            ////    new Trader { Name = "Pesho", Address = "Aleksandra Malinov 203", NumberOfShops = 4 },
            ////    new Trader { Name = "Gosho", Address = "Aleksandra Malinov 203", NumberOfShops = 3 },
            ////    new Trader { Name = "Trajko", Address = "Aleksandra Malinov 203", NumberOfShops = 2 },
            ////    new Trader { Name = "Marko", Address = "Aleksandra Malinov 203", NumberOfShops = 3 });

            context.Types.AddOrUpdate(
                t => t.TypeName,
                new Chemicals.Models.Type { TypeName = "Acid" },
                new Chemicals.Models.Type { TypeName = "Organic" },
                new Chemicals.Models.Type { TypeName = "Alcali" });

            context.Measures.AddOrUpdate(
                m => m.MeasureName,
                new Measure { MeasureName = "Liters" },
                new Measure { MeasureName = "Kg" });
        }
    }
}
