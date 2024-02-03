using BookStoreApp.Core.Repositories;
using BookStoreApp.Persistence.Contexts;
using BookStoreApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookStoreApp.Persistence.Extensions.Microsoft
{
    public static class DependencyResolvers
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration) => services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("SqlConnection"), options =>
            {
                options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
            });
        });
        public static void ConfigurePersistenceRepositories(this IServiceCollection services)
        {

            services.AddScoped<IRepositoryManager,RepositoryManager>();
        }
        
    }
}
