namespace Chemicals.ReportingServices
{
    using Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    
    using MySqlData.Models;

    public class ExportSQLToJSON
    {
        public static List<Report> ImportProductsInfo(string directoryPath)
        {
            var files = Directory.GetFiles(directoryPath);

            string jsonReport;
            var copy = new List<Report>();

            for (int i = 0; i < files.Length; i++)
            {
                jsonReport = File.ReadAllText(files[i]);
                copy.Add(JsonConvert.DeserializeObject<Report>(jsonReport));
            }

            return copy;
        }

        public static void ExportProducts(List<Product> dbSet, string filePath)
        {
            var firstProduct = dbSet;
            //var jsonStrBuilder = new StringBuilder();

            string jsonReport;

            foreach (var item in dbSet)
            {
                foreach (var sale in item.Sales)
                {
                    var itemToJson = new
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Type = item.Type.TypeName,
                        Vendor = sale.Trader.Name,
                        PricePerUnit = item.PricePerUnit,
                        Sold = sale.Quantity,
                        TotalIncome = sale.Quantity * item.PricePerUnit
                    };

                    //jsonStrBuilder.Append(JsonConvert.SerializeObject(itemToJson, Formatting.Indented));
                    //jsonReport = jsonStrBuilder.ToString();

                    jsonReport = JsonConvert.SerializeObject(itemToJson, Formatting.Indented);

                    System.Console.WriteLine(jsonReport);

                    File.WriteAllText(filePath + item.Id.ToString() + "_jsonReport.txt", jsonReport);
                }
            }
        }
    }
}
