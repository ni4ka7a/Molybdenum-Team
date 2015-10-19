namespace Chemicals.XmlData
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    using Models;

    public class XmlDataImporter
    {
        private XmlSerializer serializer;

        public XmlDataImporter()
        {
        }

        public ICollection<Trader> LoadTraders(string path)
        {
            this.serializer = new XmlSerializer(typeof(List<Trader>));

            using (var reader = new StreamReader(path))
            {
                var traders = this.serializer.Deserialize(reader) as List<Trader>;
                return traders;
            }
        }

        public ICollection<Manufacturer> LoadManufecturers(string path)
        {
            this.serializer = new XmlSerializer(typeof(List<Manufacturer>));

            using (var reader = new StreamReader(path))
            {
                var manufacturers = this.serializer.Deserialize(reader) as List<Manufacturer>;
                return manufacturers;
            }
        }
    }
}
