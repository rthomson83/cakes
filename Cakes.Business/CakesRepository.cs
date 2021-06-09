using System.Collections.Generic;
using System.Threading.Tasks;
using Cakes.Data;
using Cakes.Models;

namespace Cakes.Business
{
    public class CakesRepository : ICakesRepository
    {
        private readonly CakesContext _context;

        public CakesRepository(CakesContext context)
        {
            _context = context;
        }
        
        public Task<List<Cake>> GetCakesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Cake> GetCakeAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Cake> AddCakeAsync(Cake cake)
        {
            throw new System.NotImplementedException();
        }

        public Task<Cake> UpdateCakeAsync(Cake cake)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveCakeAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CakeExistsAsync(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}