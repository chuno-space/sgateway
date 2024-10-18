using CHUNO.SGateway;
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

builder.AddGatewayServices();

var app = builder.Build();


app.MapGateway();


app.MapGet("/healthcheck", async () =>
{
    return $"SGateway: OK at {DateTime.Now}";
});

app.Run();