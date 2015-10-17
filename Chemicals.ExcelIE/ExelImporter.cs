namespace Chemicals.ExcelImporter
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Reflection;

    using Chemicals.ExcelImporter.Contracts;

    public class ExelImporter
    {
        private IZipExtractor zipExtractor;

        public ExelImporter()
        {
            this.zipExtractor = new ZipExtractor();
        }

        public ICollection<T> ImportModelsDataFromZipFile<T>(string zipFilePath, string extractToPath = "./tests") where T : new()
        {
            this.zipExtractor.Extract(zipFilePath, extractToPath);
            return this.ImportModelsDataFromDirectory<T>(extractToPath);
        }

        public ICollection<T> ImportModelsDataFromDirectory<T>(string directoryPath) where T : new()
        {
            IEnumerable<string> filePaths = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
            ICollection<T> importedModel = new HashSet<T>();

            foreach (var path in filePaths)
            {
                foreach (var model in this.ImportModelsDataFromFile<T>(path))
                {
                    importedModel.Add(model);
                }
            }

            return importedModel;
        }

        public ICollection<T> ImportModelsDataFromFile<T>(string filePath) where T : new()
        {
            OleDbConnection connection = new OleDbConnection();

            string connectionString = ConfigurationManager.ConnectionStrings["ExcelChemicalsData"].ConnectionString;
            connection.ConnectionString = string.Format(connectionString, filePath);

            connection.Open();

            using (connection)
            {
                var schema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                var sheetName = schema.Rows[0]["TABLE_NAME"].ToString();

                OleDbCommand selectAllRowsCommand = new OleDbCommand("SELECT * FROM [" + sheetName + "]", connection);

                ICollection<T> importedModels = new HashSet<T>();

                using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectAllRowsCommand))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    using (DataTableReader reader = dataSet.CreateDataReader())
                    {
                        importedModels = this.MapDataToModelsCollection<T>(reader);
                    }
                }

                return importedModels;
            }
        }

        private ICollection<T> MapDataToModelsCollection<T>(IDataReader dr) where T : new()
        {
            Type businessEntityType = typeof(T);
            ICollection<T> entitys = new HashSet<T>();
            Dictionary<string, PropertyInfo> hashtable = new Dictionary<string, PropertyInfo>();
            PropertyInfo[] properties = businessEntityType.GetProperties();

            foreach (PropertyInfo info in properties)
            {
                hashtable[info.Name.ToUpper()] = info;
            }

            while (dr.Read())
            {
                T newObject = new T();
                for (int index = 0; index < dr.FieldCount; index++)
                {
                    PropertyInfo info = hashtable[dr.GetName(index).ToUpper()];
                    if ((info != null) && info.CanWrite)
                    {
                        var k = dr.GetValue(index);
                        var v = Convert.ChangeType(k, info.PropertyType);
                        info.SetValue(newObject, v, null);
                    }
                }

                entitys.Add(newObject);
            }

            dr.Close();
            return entitys;
        }
    }
}
