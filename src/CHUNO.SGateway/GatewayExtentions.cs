using CHUNO.AuthService.Constract.Proto;
using CHUNO.SGateway.Infrastructures;
using Grpc.Net.Client;
using Yarp.ReverseProxy.Configuration;

namespace CHUNO.SGateway
{
    public static class GatewayExtentions
    {
        public static void AddGatewayServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            var configration = builder.Configuration.GetSection("ReverseProxy");
            var proxyBuilder = services.AddReverseProxy();

            proxyBuilder.Services.AddSingleton<IProxyConfigProvider>(sp =>
            {
                var gateProxySource = new GatewayProxySource(configration);
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
