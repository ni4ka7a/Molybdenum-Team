namespace Chemicals.MongoData.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using Chemicals.Models;

    public class MongoExporter
    {
        private const string TradersCollectionName = "Traders";
        private const string ManufecturersCollectionName = "Manufecturers";

        public MongoExporter()
        {
        }

        public void ImportTraders(IMongoDatabase database, ICollection<Trader> traders)
        {
            var collection = database.GetCollection<Trader>(TradersCollectionName);

            collection.InsertManyAsync(traders).Wait();
        }

        public void ImportManufacturers(IMongoDatabase database, ICollection<Manufacturer> manufacturers)
        {
            var collection = database.GetCollection<Manufacturer>(ManufecturersCollectionName);

            collection.InsertManyAsync(manufacturers).Wait();
        }
    }
}
