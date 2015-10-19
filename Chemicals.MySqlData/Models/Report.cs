namespace Chemicals.MySqlData.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Report
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Vendor { get; set; }

        public string PricePerUnit { get; set; }

        public string Sold { get; set; }

        public string TotalIncome { get; set; }
    }
}
