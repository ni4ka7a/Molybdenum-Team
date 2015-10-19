namespace Chemicals.Models
{
    using System;

    public class Sale
    {
        public int Id { get; set; }

        public int TraderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime SaleDate { get; set; }

        public virtual Trader Trader { get; set; }

        public virtual Product Product { get; set; }
    }
}
