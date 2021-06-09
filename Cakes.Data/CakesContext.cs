using Cakes.Models;
using Cakes.Models.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cakes.Data
{
    public class CakesContext
    {
        private readonly IMongoDatabase _database;

        public CakesContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Name);
        }

        public IMongoCollection<Cake> Cakes => _database.GetCollection<Cake>("Cake");
    }
}