namespace Chemicals.Models
{
    using System;

    public class Produce
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int ManufacturerId { get; set; }

        public int Amount { get; set; }

        public DateTime ProducedDate { get; set; }

        public virtual Product Product { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
