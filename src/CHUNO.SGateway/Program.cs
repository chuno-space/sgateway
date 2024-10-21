using CHUNO.SGateway;
using CHUNO.SGateway.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    // public
    options.Listen(IPAddress.Any, 9090, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1;
    });

    // internal
    options.Listen(IPAddress.Any, 9091, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
        listenOptions.UseHttps();
    });

});

//builder.AddOpenApi();
builder.AddGatewayServices();
builder.Services.AddGrpc();

var app = builder.Build();


//app.UseOpenApi();
app.UseGateWay();
app.UseInternalGrpc("*:9091");

app.MapGet("/health", async ([FromServices] GatewayDBContext dBContext) =>
{
    var db = dBContext.Database;
    var ok = await db.CanConnectAsync();
    return $"SGateway: OK:{ok} at {DateTime.Now}";
});

app.Run();