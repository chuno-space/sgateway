using CHUNO.AuthService.Constract.Proto;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 9090, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1;
    });

});
// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/healthcheck", async () =>
{
    return $"OK at {DateTime.Now}";
});

var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new Greeter.GreeterClient(channel);
app.MapGet("/grpc", async () =>
{
    var reply = await client.SayHelloAsync(
                      new HelloRequest { Name = "GreeterClient" });
    Console.WriteLine("Greeting: " + reply.Message);
    return reply.Message;
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
