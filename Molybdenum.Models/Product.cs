namespace Chemicals.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        private ICollection<Produce> produces;
        private ICollection<Sale> sales;

        public Product()
        {
            this.produces = new HashSet<Produce>();
            this.sales = new HashSet<Sale>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [Index(IsUnique = true)] 
        public string Name { get; set; }

        public int TypeId { get; set; }

        public int MeasureId { get; set; }

        [MaxLength(50)]
        public string Formula { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal PricePerUnit { get; set; }

        [StringLength(200, MinimumLength = 5)]
        public string DescriptionOfProduction { get; set; }

        [StringLength(200, MinimumLength = 5)]
        public string DescriptionOfStorage { get; set; }

        public virtual Measure Measure { get; set; }

        public virtual Type Type { get; set; }

        public virtual ICollection<Produce> Produces
        {
            get
            {
                return this.produces;
            }

            set
            {
                this.produces = value;
            }
        }

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
