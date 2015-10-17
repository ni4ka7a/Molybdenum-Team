namespace Chemicals.Models
{
    public class Produce
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int ManufacturerId { get; set; }

        public uint Amount { get; set; }

        public virtual Product Product { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
