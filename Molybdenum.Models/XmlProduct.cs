namespace Chemicals.Models
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class XmlProduct
    {
        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public int Amout { get; set; }

        [XmlElement]
        public string Formula { get; set; }
    }
}
