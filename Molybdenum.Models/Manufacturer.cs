namespace Chemicals.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Manufacturer
    {
        private ICollection<Produce> produces;

        public Manufacturer()
        {
            this.produces = new HashSet<Produce>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 2)]
        [Index(IsUnique = true)] 
        public string Name { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 5)]
        public string Address { get; set; }

        public int NumberOfFactories { get; set; }

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
    }
}
