namespace Chemicals.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Type
    {
        private ICollection<Product> products;

        public Type()
        {
            this.products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string TypeName { get; set; }

        public virtual ICollection<Product> Products
        {
            get
            {
                return this.products;
            }

            set
            {
                this.products = value;
            }
        } 
    }
}
