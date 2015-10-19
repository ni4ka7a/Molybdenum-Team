namespace Chemicals.ExcelDataIE
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.OleDb;
    using System.IO;
    using System.Reflection;

    using Chemicals.ExcelImporter.Contracts;

    public class ExcelImporter<T> where T : new()
    {
        private IZipExtractor zipExtractor;

        public ExcelImporter(IZipExtractor zippExtractor)
        {
            this.zipExtractor = zippExtractor;
        }

        public ICollection<T> ImportModelsDataFromZipFile(string zipFilePath, string extractToPath = "./tests")
        {
            this.zipExtractor.Extract(zipFilePath, extractToPath);
            return this.ImportModelsDataFromDirectory(extractToPath);
        }

        public ICollection<T> ImportModelsDataFromDirectory(string directoryPath) 
        {
            IEnumerable<string> filePaths = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
            ICollection<T> importedModel = new HashSet<T>();

            foreach (var path in filePaths)
            {
                foreach (var model in this.ImportModelsDataFromFile(path))
                {
                    importedModel.Add(model);
                }
            }

            return importedModel;
        }

        public ICollection<T> ImportModelsDataFromFile(string filePath)
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
                        importedModels = this.MapDataToModelsCollection(reader);
                    }
                }

                return importedModels;
            }
        }

        private ICollection<T> MapDataToModelsCollection(IDataReader dataReader)
        {
            Type businessEntityType = typeof(T);
            ICollection<T> modelsCollection = new HashSet<T>();
            Dictionary<string, PropertyInfo> propertyDictionary = new Dictionary<string, PropertyInfo>();
            PropertyInfo[] properties = businessEntityType.GetProperties();

            foreach (PropertyInfo info in properties)
            {
                propertyDictionary[info.Name] = info;
            }

            while (dataReader.Read())
            {
                T newObject = new T();
                for (int index = 0; index < dataReader.FieldCount; index++)
                {
                    PropertyInfo info = propertyDictionary[dataReader.GetName(index)];
                    if ((info != null) && info.CanWrite)
                    {
                        var k = dataReader.GetValue(index);
                        var v = Convert.ChangeType(k, info.PropertyType);
                        info.SetValue(newObject, v, null);
                    }
                }

                modelsCollection.Add(newObject);
            }

            dataReader.Close();
            return modelsCollection;
        }
    }
}
