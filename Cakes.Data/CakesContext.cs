using System.Threading.Tasks;
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
        public IMongoCollection<Counter> Counters => _database.GetCollection<Counter>("Counter");

        public async Task<int> GetNextSequenceAsync(string name)
        {
            var filter = Builders<Counter>.Filter.Eq(x => x.Id, name);
            var update = Builders<Counter>.Update.Inc(x => x.Sequence, 1);
            return (await Counters.FindOneAndUpdateAsync(filter, update)).Sequence;
        }

        public async Task SeedDataAsync()
        {
            if (await Cakes.CountDocumentsAsync(FilterDefinition<Cake>.Empty) == 0)
            {
                // Seed some test data
            }

            if (await Counters.CountDocumentsAsync(FilterDefinition<Counter>.Empty) == 0)
            {
                // Seed initial counter sequence
                await Counters.InsertOneAsync(new Counter
                {
                    Id = "id",
                    Sequence = 1
                });
            }
        }
    }
}