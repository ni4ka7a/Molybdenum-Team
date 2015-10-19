namespace Chemicals.SQLightData
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RadioactiveProducts")]
    public class RadioactiveProduct
    {
        [Key]
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int Radioactivity { get; set; }
    }
}
