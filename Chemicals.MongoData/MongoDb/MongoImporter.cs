namespace Chemicals.MongoData.MongoDb
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
        private const string TradersCollectionName = "Traders";
        private const string ManufecturersCollectionName = "Manufecturers";

        public MongoImporter()
        {
        }

        public ICollection<Product> GetAllProducts(IMongoDatabase database, string collectionName)
        {
            var collection = database.GetCollection<Product>(collectionName);
            var rest = collection.Find(_ => true).ToListAsync().Result;

            return rest;
        }

        public ICollection<Trader> GetAllTraders(IMongoDatabase database)
        {
            var collection = database.GetCollection<Trader>(TradersCollectionName);
            var rest = collection.Find(t => true).ToListAsync().Result;

            return rest;
        }

        public ICollection<Manufacturer> GetAllManufacturers(IMongoDatabase database)
        {
            var collection = database.GetCollection<Manufacturer>(ManufecturersCollectionName);
            var rest = collection.Find(t => true).ToListAsync().Result;

            return rest;
        }

        public void ImportManufacturers(IMongoDatabase database, ICollection<Manufacturer> manufacturers)
        {
            var collection = database.GetCollection<Manufacturer>(ManufecturersCollectionName);

            collection.InsertManyAsync(manufacturers).Wait();
        }

        public void ImportTraders(IMongoDatabase database, ICollection<Trader> traders)
        {
            var collection = database.GetCollection<Trader>(ManufecturersCollectionName);

            collection.InsertManyAsync(traders).Wait();
        }
    }
}
