using Microsoft.Extensions.DependencyInjection;

namespace CHUNO.Framework.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceGrpc(this IServiceCollection services)
        {
            services.AddGrpc();
            return services;
        }
    }
}
