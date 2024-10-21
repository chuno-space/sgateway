using CHUNO.Framework.Core.Intefaces;
using CHUNO.Framework.Domain.Events;
using CHUNO.Framework.Infrastructure.Authentication;
using CHUNO.Framework.Infrastructure.Common;
using CHUNO.Framework.Infrastructure.Messaging.Behaviors;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace CHUNO.Framework.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrasCore(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IDateTime, DefaultDateTime>();
            services.AddTransient<IEventDispatcher, DefaultEventDispatcher>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));

                cfg.AddOpenBehavior(typeof(TransactionBehaviour<,>));
            });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //   .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            //   {
            //       ValidateIssuer = true,
            //       ValidateAudience = true,
            //       ValidateLifetime = true,
            //       ValidateIssuerSigningKey = true,
            //       ValidIssuer = configuration["Jwt:Issuer"],
            //       ValidAudience = configuration["Jwt:Audience"],
            //       IssuerSigningKey = new SymmetricSecurityKey(
            //               Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]))
            //   });

            //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SettingsKey));

            //services.Configure<MailSettings>(configuration.GetSection(MailSettings.SettingsKey));

            //services.Configure<MessageBrokerSettings>(configuration.GetSection(MessageBrokerSettings.SettingsKey));

            //services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();

            //services.AddScoped<IJwtProvider, JwtProvider>();


            //services.AddTransient<IPasswordHasher, PasswordHasher>();

            //services.AddTransient<IPasswordHashChecker, PasswordHasher>();

            //services.AddTransient<IEmailService, EmailService>();

            //services.AddTransient<IEmailNotificationService, EmailNotificationService>();

            //services.AddSingleton<IIntegrationEventPublisher, IntegrationEventPublisher>();

            return services;
        }
        public static IServiceCollection AddServiceGrpc(this IServiceCollection services)
        {
            services.AddGrpc();
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            services.AddHttpContextAccessor();
            services.AddScoped<IUserProvider, DefaultUserProvider>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   //options.TokenValidationParameters = new TokenValidationParameters
                   //{
                   //    ValidateLifetime = true,
                   //    ValidateIssuer = true,
                   //    ValidIssuer = "dotnet-user-jwts",
                   //    ValidateAudience = true,
                   //    ValidAudience = "sgateway",
                   //    ValidateIssuerSigningKey = true,
                   //    //dotnet user-jwts key
                   //    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("pUzDk0u5icVP1BBZiZL3rbaD5UbTyOOddpRKqEMXdUY="))
                   //};
               });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("tst", builder =>
                {
                    builder.RequireAssertion(context =>
                    {
                        return context.User != null;
                    });
                });
            });
            return services;
        }
    }
}
