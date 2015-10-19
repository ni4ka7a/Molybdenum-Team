namespace Chemicals.Models
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class XmlManufacturer
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlArrayItem("Product")]
        public List<XmlProduct> Products { get; set; }
    }
}
