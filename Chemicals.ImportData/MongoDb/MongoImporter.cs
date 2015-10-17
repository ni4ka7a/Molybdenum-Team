namespace Chemicals.ImportData.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Chemicals.Models;
    using MongoDB.Driver;

    public class MongoImporter
    {
        public MongoImporter()
        {
        }

        public ICollection<Product> GetAllProducts(IMongoDatabase database, string collectionName)
        {
            var collection = database.GetCollection<Product>(collectionName);
            var rest = collection.Find(_ => true).ToListAsync().Result;

            return rest;
        }
    }
}
