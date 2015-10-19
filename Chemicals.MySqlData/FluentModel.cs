namespace Chemicals.MySqlData
{
    using System.Collections.Generic;

    using Models;
    using Telerik.OpenAccess.Metadata.Fluent;

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
