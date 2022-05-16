using FI.Data;
using Microsoft.Extensions.DependencyInjection;

namespace FI.API.DependencyRegistration
{
    public static class Services
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<FIContext, FIContext>();
        }
    }
}
