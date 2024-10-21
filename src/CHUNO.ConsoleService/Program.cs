using CHUNO.ConsoleService;
using CHUNO.ConsoleService.Boundaries.Grpc;
using CHUNO.Framework.Infrastructure;
using CHUNO.Framework.Infrastructure.Authentication;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5020, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1;
    });

    options.Listen(IPAddress.Any, 5021, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
        listenOptions.UseHttps();
        //listenOptions.UseHttps("<path to .pfx file>",
        //    "<certificate password>");
    });
});

builder.Services.AddInfrasCore(builder.Configuration);
builder.Services.AddAuth();

builder.Services.AddServiceGrpc();
builder.Services.AddGrpcClients();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Map for native-grpc
app.MapGrpcService<GrpcAppClientService>()
.RequireHost("*:5021");

app.MapGet("/health", async () =>
{
    return $"ConsoleService: OK at {DateTime.Now}";
});

app.MapGet("/test", async (IUserProvider userProvider) =>
{
    var user = userProvider.User;
    return $"ConsoleService: OK at {DateTime.Now} : {user?.UserId}";
});

app.Run();
