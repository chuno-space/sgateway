using CHUNO.Framework.Core.Intefaces;
using CHUNO.Framework.Domain.Events;
using CHUNO.Framework.Infrastructure.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CHUNO.Framework.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrasCore(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IDateTime, DefaultDateTime>();
            services.AddTransient<IEventDispatcher, DefaultEventDispatcher>();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                //cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));

                //cfg.AddOpenBehavior(typeof(TransactionBehaviour<,>));
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
    }
}
