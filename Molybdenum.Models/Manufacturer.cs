namespace Chemicals.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
        public string Name { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 5)]
        public string Address { get; set; }

        public uint NumberOfFactories { get; set; }

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
