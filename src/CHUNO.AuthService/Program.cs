using CHUNO.AuthService.Boundaries.Grpc;
using CHUNO.Framework.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5000, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1;
    });

    options.Listen(IPAddress.Any, 5001, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
        listenOptions.UseHttps();
        //listenOptions.UseHttps("<path to .pfx file>",
        //    "<certificate password>");
    });
});

// Add services to the container.
builder.Services.AddServiceGrpc();

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseGrpcWeb();
app.UseCors();

app.MapGet("/health", async () =>
{
    return $"AuthService: OK at {DateTime.Now}";
});

app.MapGet("/test", async () =>
{
    return $"AuthService: OK at {DateTime.Now}";
});
// Map for native-grpc
app.MapGrpcService<GrpcUserService>()
.RequireHost("*:5001");

// Map for  grpc-web
app.MapGrpcService<GrpcUserService>()
    .RequireHost("*:5000") 
    .EnableGrpcWeb()
    .RequireCors("AllowAll");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
