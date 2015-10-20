namespace Chemicals.SQLightData
{
    using System.Data.Entity;

    public class RadioactivityDbContext : DbContext
    {
        public RadioactivityDbContext()
            : base("RadioactivityDb")
        {
            this.Database.CreateIfNotExists();
        }

        public DbSet<RadioactiveProduct> RadioactiveProducts { get; set; }
    }
}
