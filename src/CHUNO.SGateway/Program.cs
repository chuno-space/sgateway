using CHUNO.SGateway;
using CHUNO.SGateway.Data;
using Microsoft.AspNetCore.Mvc;
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

//builder.AddOpenApi();
builder.AddGatewayServices();

var app = builder.Build();

//app.UseOpenApi();
app.UseGateWay();

app.MapGet("/health", async ([FromServices] GatewayDBContext dBContext) =>
{
    var db = dBContext.Database;
    var ok = await db.CanConnectAsync();
    return $"SGateway: OK:{ok} at {DateTime.Now}";
});

app.Run();