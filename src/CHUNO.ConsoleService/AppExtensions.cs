extern alias SGatewayConstract;

using CHUNO.ConsoleService.Services;
using SGatewayClient = SGatewayConstract.CHUNO.SGateway.Constract.Proto.SGateway.SGatewayClient;

namespace CHUNO.ConsoleService
{
    public static class AppExtensions
    {
        public static IServiceCollection AddGrpcClients(this IServiceCollection services)
        {
            // https://learn.microsoft.com/en-us/aspnet/core/grpc/clientfactory?view=aspnetcore-8.0
            services.AddGrpcClient<SGatewayClient>(o =>
             {
                 o.Address = new Uri("https://localhost:9091");
             });


            services.AddScoped<GatewayService>();
            return services;
        }
    }
}
