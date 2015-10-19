namespace Chemicals.ExcelDataIE
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    using OfficeOpenXml;

    public class ExcelExporter<T> where T : new()
    {
        public void ExportDataModelsCollectionToExcelFile(
            string saveToPath,
            string fileName,
            string worksheetName,
            ICollection<T> modelsCollection)
        {
            Type businessEntityType = typeof(T);
            PropertyInfo[] properties = businessEntityType.GetProperties();

            FileInfo newFile = new FileInfo(saveToPath + @"\" + fileName + ".xlsx");

            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetName);

                // Add the headers
                for (var i = 0; i < properties.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = properties[i].Name;
                }

                int j = 2;
                int k;
                foreach (T model in modelsCollection)
                {
                    k = 1;
                    foreach (var property in properties)
                    {
                        worksheet.Cells[j, k].Value = model.GetType().GetProperty(property.Name).GetValue(model, null);
                        k++;
                    }

                    j++;
                }

                package.Save();
            }
        }
    }
}
