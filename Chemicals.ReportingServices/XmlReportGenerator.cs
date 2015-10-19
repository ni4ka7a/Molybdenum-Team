namespace Chemicals.XmlReport
{
    using System.IO;
    using System.Xml.Serialization;

    using Models;

    public class XmlReportGenerator
    {
        public void ExportXmlReport(XmlReportModel manufacturers, string pathToSave)
        {
            var serializer = new XmlSerializer(typeof(XmlReportModel));
            using (TextWriter writer = new StreamWriter(pathToSave))
            {
                serializer.Serialize(writer, manufacturers);
            }
        }
    }
}
