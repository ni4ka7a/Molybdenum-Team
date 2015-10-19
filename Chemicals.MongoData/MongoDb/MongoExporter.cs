namespace Chemicals.MongoData.MongoDb
{
    using System.Collections.Generic;

    using Models;
    using MongoDB.Driver;

    public class MongoExporter
    {
        private const string TradersCollectionName = "Traders";
        private const string ManufecturersCollectionName = "Manufecturers";

        public MongoExporter()
        {
        }

        public void ExportTraders(IMongoDatabase database, ICollection<Trader> traders)
        {
            var collection = database.GetCollection<Trader>(TradersCollectionName);

            collection.InsertManyAsync(traders).Wait();
        }

        public void ExportManufacturers(IMongoDatabase database, ICollection<Manufacturer> manufacturers)
        {
            var collection = database.GetCollection<Manufacturer>(ManufecturersCollectionName);

            collection.InsertManyAsync(manufacturers).Wait();
        }
    }
}
