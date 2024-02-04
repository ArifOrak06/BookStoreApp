using BookStoreApp.Core.Services;
using BookStoreApp.Service.Services;
using BookStoreApp.Service.Utilities.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreApp.Service.Extensions.Microsoft
{
    public static  class DependencyResolversForServices
    {
        public static void ConfigureServicesForBusinesLayer(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddSingleton<ILoggerService,LoggerManager>(); // Singletonb sadece bir kez oluşacak olan yapıyı tüm uygulama kullanacak.
            services.AddAutoMapper(typeof(BookProfile));
        }
    }
}
