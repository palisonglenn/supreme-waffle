using HowToDoBlazorSystem.BLL;
using HowToDoBlazorSystem.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HowToDoBlazorSystem.ViewModels
{
    public static class HowToDoBlazorExtension
    {
        public static void AddBackendDependencies(this IServiceCollection services,
          Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<GroceryListContext>(options);

            services.AddTransient<HowToDoBlazorService>((ServiceProvider) =>
            {
                var context = ServiceProvider.GetService<GroceryListContext>();
                return new HowToDoBlazorService(context);
            });
        }
    }
}
