namespace Chemicals.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Trader
    {
        private ICollection<Sale> sales;

        public Trader()
        {
            this.sales = new HashSet<Sale>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Index(IsUnique = true)] 
        public string Name { get; set; }

        [MaxLength(70, ErrorMessage = "Maximum length of trader address is 70 chars!")]
        public string Address { get; set; }

        public int NumberOfShops { get; set; }

        public virtual ICollection<Sale> Sales
        {
            get
            {
                return this.sales;
            }

            set
            {
                this.sales = value;
            }
        }
    }
}
