using CHUNO.AuthService.Constract.Proto;
using CHUNO.Framework.Data.Core;
using CHUNO.SGateway.Data;
using CHUNO.SGateway.Infrastructures;
using CHUNO.Framework.Infrastructure;
using Grpc.Net.Client;
using Microsoft.EntityFrameworkCore;
using Yarp.ReverseProxy.Configuration;

namespace CHUNO.SGateway
{
    public static class GatewayExtentions
    {
        public static void AddGatewayServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddInfrasCore(configuration);

            string connectionString = "Data Source=LocalDatabase.db";// configuration.GetConnectionString("")!;
            // https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/
            //https://stackoverflow.com/questions/65022729/use-both-adddbcontextfactory-and-adddbcontext-extension-methods-in-the-same
            services.AddDbContextFactory<GatewayDBContext>(optionsAction => 
                optionsAction.UseSqlite(connectionString)
               );
            services.AddDbContext<GatewayDBContext>(options => 
                options.UseSqlite(connectionString),
                optionsLifetime: ServiceLifetime.Singleton
                );
            //services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<GatewayDBContext>());
            //services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<GatewayDBContext>());

         
            var configration = builder.Configuration.GetSection("ReverseProxy");
            services.AddSingleton(sp => new GatewayProxySource(
                sp.GetRequiredService<IDbContextFactory<GatewayDBContext>>(), 
                configration)
            );

            var proxyBuilder = services.AddReverseProxy()
                .ConfigureHttpClient((context, handler) =>
                {
                    //var clientCert = new X509Certificate2("path");
                    //handler.SslOptions.ClientCertificates?.Add(clientCert);
                });

            proxyBuilder.Services.AddSingleton<IProxyConfigProvider>(sp =>
            {
                var gateProxySource = sp.GetRequiredService<GatewayProxySource>();
                return new GatewayProxyConfigProvider(
                    sp.GetRequiredService<ILogger<GatewayProxyConfigProvider>>(),
                    gateProxySource);
            });
        }

        public static void MapGateway(this WebApplication app) {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            app.MapGet("/grpc", async () =>
            {
                var reply = await client.SayHelloAsync(
                                  new HelloRequest { Name = "GreeterClient" });
                Console.WriteLine("Greeting: " + reply.Message);
                return reply.Message;
            });

            app.MapReverseProxy(builder =>
            {
            });
        }
    }
}
