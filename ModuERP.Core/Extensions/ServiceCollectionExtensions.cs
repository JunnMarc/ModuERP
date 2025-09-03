using Microsoft.Extensions.DependencyInjection;
using ModuERP.Core.Interfaces;
using ModuERP.Core.Services;

namespace ModuERP.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
