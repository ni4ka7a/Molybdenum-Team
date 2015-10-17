namespace Chemicals.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public int TraderId { get; set; }

        public int ProductId { get; set; }

        public uint Quantity { get; set; }

        public virtual Trader Tradet { get; set; }

        public virtual Product Product { get; set; }
    }
}
