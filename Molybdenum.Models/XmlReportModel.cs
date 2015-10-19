namespace Chemicals.Models
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    [XmlRoot("ManufacturersProduction")]
    public class XmlReportModel
    {
        [XmlArrayItem("Manufacturer")]
        public List<XmlManufacturer> Manufacturers { get; set; }
    }
}
