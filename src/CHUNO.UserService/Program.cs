var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.


app.MapGet("/health", async () =>
{
    return $"UserService: OK at {DateTime.Now}";
});

app.MapGet("/test", async () =>
{
    return $"UserService: OK at {DateTime.Now}";
});

app.Run();
