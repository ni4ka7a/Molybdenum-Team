namespace Chemicals.Data
{
    using System.Data.Entity;

    using Chemicals.Models;

    public class ChemicalsDbContext : DbContext
    {
        public ChemicalsDbContext()
            : base("ChemicalsDb")
        {
        }

        public virtual IDbSet<Product> Products { get; set; }

        public virtual IDbSet<Manufacturer> Manufacturers { get; set; }

        public virtual IDbSet<Measure> Measures { get; set; }

        public virtual IDbSet<Produce> Produces { get; set; }

        public virtual IDbSet<Sale> Sales { get; set; }

        public virtual IDbSet<Trader> Traders { get; set; }

        public virtual IDbSet<Type> Types { get; set; }
    }
}
