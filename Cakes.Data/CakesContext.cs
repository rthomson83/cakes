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
            if (await Counters.CountDocumentsAsync(FilterDefinition<Counter>.Empty) == 0)
            {
                // Seed initial counter sequence
                await Counters.InsertOneAsync(new Counter
                {
                    Id = "id",
                    Sequence = 1
                });
            }
            
            if (await Cakes.CountDocumentsAsync(FilterDefinition<Cake>.Empty) == 0)
            {
                await Cakes.InsertManyAsync(new[]
                {
                    new Cake
                    {
                        Id = await GetNextSequenceAsync("id"),
                        Name = "Test Cake 1",
                        Comment =
                            "Cupcake ipsum dolor sit amet tiramisu apple pie jujubes. Caramels toffee gummi bears gummi bears. Macaroon jelly-o marshmallow toffee chocolate bar fruitcake gummi bears chocolate cake cookie. Powder liquorice sesame snaps gummies.",
                        ImageUrl =
                            "https://media.istockphoto.com/photos/coffee-and-cake-picture-id155131783?k=6&m=155131783&s=612x612&w=0&h=NU-wDV7_iNLn86nKG3-FQt8REuw-75aGEW9SRLFcunc=",
                        YumFactor = 5
                    },
                    new Cake
                    {
                        Id = await GetNextSequenceAsync("id"),
                        Name = "Test Cake 2",
                        Comment =
                            "Cupcake ipsum dolor sit amet wafer dragée powder jelly-o. Jelly-o pastry icing biscuit jujubes apple pie croissant gummi bears. Danish dessert chocolate liquorice. Icing caramels sesame snaps jelly dessert liquorice lemon drops.",
                        ImageUrl =
                            "https://media.istockphoto.com/photos/vanilla-cupcakes-with-pink-yellow-and-blue-icing-isolated-picture-id177047298?k=6&m=177047298&s=612x612&w=0&h=DRwDoHXkR6ZHQ2qDZL5GoealXoTvDi2eMhiE5nJ9H1Q=",
                        YumFactor = 4
                        
                    },
                    new Cake
                    {
                        Id = await GetNextSequenceAsync("id"),
                        Name = "Test Cake 3",
                        Comment =
                            "Cupcake ipsum dolor sit amet lollipop. Sweet marshmallow marshmallow jujubes bear claw. Macaroon chocolate jelly toffee. Dragée cheesecake croissant I love danish.",
                        ImageUrl =
                            "https://media.istockphoto.com/photos/cheesecake-and-cappuchino-for-dessert-picture-id1166130822?k=6&m=1166130822&s=612x612&w=0&h=06qtm_SbUNuDC3ZK2rQYAO9P1GM3wbfcEpOjdBfYh80=",
                        YumFactor = 3
                    },
                    new Cake
                        {
                            Id = await GetNextSequenceAsync("id"),
                            Name = "Test Cake 4",
                            Comment =
                                "Cupcake ipsum dolor sit amet cake pudding. Chocolate bar icing marzipan brownie chocolate bar soufflé sesame snaps. Icing jelly-o bear claw I love chupa chups.",
                            ImageUrl =
                                "https://media.istockphoto.com/photos/detail-of-small-cake-with-walnuts-and-salted-caramel-picture-id1184181774?k=6&m=1184181774&s=612x612&w=0&h=ZBt9cOTA7Pp4GzgnB6rvCWRbPvTEu14KkQeOx1omroI=",
                            YumFactor = 1
                        }
                });
            }
        }
    }
}