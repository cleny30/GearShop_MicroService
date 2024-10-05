using WebClient.Service;

namespace WebClient.Core
{
    public static class DependencyInjection
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ShopService>();
            services.AddScoped<ProductService>();
        }
    }
}
