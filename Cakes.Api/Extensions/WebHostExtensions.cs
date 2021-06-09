using System.Threading.Tasks;
using Cakes.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cakes.Api.Extensions
{
    public static class WebHostExtensions
    {
        public static async Task<IHost> SeedDataAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<CakesContext>();
            await context.SeedDataAsync();

            return host;
        }
    }
}