using System.Collections.Generic;
using System.Threading.Tasks;
using Cakes.Data;
using Cakes.Models;
using MongoDB.Driver;

namespace Cakes.Business
{
    public class CakesRepository : ICakesRepository
    {
        private readonly CakesContext _context;

        public CakesRepository(CakesContext context)
        {
            _context = context;
        }
        
        public async Task<List<Cake>> GetCakesAsync()
        {
            return await _context.Cakes.Find(FilterDefinition<Cake>.Empty).SortBy(x => x.Name).ToListAsync();
        }

        public async Task<Cake> GetCakeAsync(int id)
        {
            var filter = Builders<Cake>.Filter.Eq(x => x.Id, id);
            return await _context.Cakes.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Cake> AddCakeAsync(Cake cake)
        {
            cake.Id = await _context.GetNextSequenceAsync("id");
            await _context.Cakes.InsertOneAsync(cake);
            return cake;
        }

        public async Task<Cake> UpdateCakeAsync(Cake cake)
        {
            var filter = Builders<Cake>.Filter.Eq(x => x.Id, cake.Id);
            await _context.Cakes.ReplaceOneAsync(filter, cake);
            return cake;
        }

        public async Task RemoveCakeAsync(int id)
        {
            var filter = Builders<Cake>.Filter.Eq(x => x.Id, id);
            await _context.Cakes.DeleteOneAsync(filter);
        }

        public async Task<bool> CakeExistsAsync(string name)
        {
            var filter = Builders<Cake>.Filter.Eq(x => x.Name, name);
            return await _context.Cakes.CountDocumentsAsync(filter, new CountOptions { Collation = new Collation(locale: "en", strength: CollationStrength.Secondary)}) > 0;
        }
    }
}