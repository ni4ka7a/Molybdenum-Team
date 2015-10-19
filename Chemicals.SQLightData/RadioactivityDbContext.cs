namespace Chemicals.SQLightData
{
    using System.Data.Entity;

    public class RadioactivityDbContext : DbContext
    {
        public DbSet<RadioactiveProduct> RadioactiveProducts { get; set; }
    }
}
