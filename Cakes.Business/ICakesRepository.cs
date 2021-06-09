using System.Collections.Generic;
using System.Threading.Tasks;
using Cakes.Models;

namespace Cakes.Business
{
    public interface ICakesRepository
    {
        Task<List<Cake>> GetCakesAsync();
        Task<Cake> GetCakeAsync(int id);
        Task<Cake> AddCakeAsync(Cake cake);
        Task<Cake> UpdateCakeAsync(Cake cake);
        Task RemoveCakeAsync(int id);
        Task<bool> CakeExistsAsync(string name);
    }
}