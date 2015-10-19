namespace Chemicals.SQLightData
{
    using System.Data.Entity;

    public class RadioactivityDbContext : DbContext
    {

        public RadioactivityDbContext()
            : base("RadioactivityDb")
        {
        }
        public DbSet<RadioactiveProduct> RadioactiveProducts { get; set; }
    }
}
