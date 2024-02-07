using BookStoreApp.WebAPI.ActionFilters;

namespace BookStoreApp.WebAPI.Extensions.Microsoft
{
    public static class DependencyResolvers
    {
        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddSingleton<LogFilterAttribute>();
        }
    }
}
