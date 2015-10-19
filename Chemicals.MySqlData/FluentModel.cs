namespace Chemicals.MySqlData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Telerik.OpenAccess.Metadata.Fluent;

    using Chemicals.MySqlData.Models;

    public partial class FluentModel : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            List<MappingConfiguration> configurations =
                new List<MappingConfiguration>();

            var reportMapping = new MappingConfiguration<Report>();
            reportMapping.MapType(r => new
            {
                Id = r.Id,
                Name = r.Name,
                Type = r.Type,
                Vendor = r.Vendor,
                PricePerUnit = r.PricePerUnit,
                Sold = r.Sold,
                TotalIncome = r.TotalIncome
            }).ToTable("Reports");

            reportMapping.HasProperty(r => r.Id).IsIdentity();

            configurations.Add(reportMapping);

            return configurations;
        }
    }
}
