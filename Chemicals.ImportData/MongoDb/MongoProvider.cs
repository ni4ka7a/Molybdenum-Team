namespace Chemicals.ImportData.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MongoDB.Driver;

    public class MongoProvider
    {
        public MongoProvider(string connectionString, string databaseName)
        {
            this.ConnectionString = connectionString;
            this.DatabaseName = databaseName;
        }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(this.ConnectionString);
            var db = client.GetDatabase(this.DatabaseName);

            return db;
        }
    }
}
