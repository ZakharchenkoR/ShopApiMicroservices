using Catalog.Api.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            var databaseName = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            var collectionName = configuration.GetValue<string>("DatabaseSettings:CollectionName");

            var client = new MongoClient(connectionString);
            var dataBase = client.GetDatabase(databaseName);
            Prodacts = dataBase.GetCollection<Product>(collectionName);

            CatalogContextSeed.SeedData(Prodacts);
        }

        public IMongoCollection<Product> Prodacts { get; }
    }
}
