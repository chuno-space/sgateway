using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CHUNO.Framework.Infrastructure
{
    public static class OpenAPIExtensions
    {
        public static void AddOpenApi(this WebApplicationBuilder builder)
        {
            // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/aspnetcore-openapi?view=aspnetcore-8.0&tabs=visual-studio%2Cminimal-apis
            // https://github.com/microsoft/reverse-proxy/issues/1789

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        public static void UseOpenApi(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseStaticFiles();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

        }
    }
}
