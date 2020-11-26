using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Exceptions;
using Infrastructure.MessageBus;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Infrastructure.Streaming;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpotifyAPI.Web;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        internal const string ConfigurationSection = "Database";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool useInMemoryDatabase)
        {
            var configSection = configuration.GetSection(ConfigurationSection);
            if (!configSection.Exists())
            {
                throw new ConfigurationSectionMissingException(ConfigurationSection);
            }
            var databaseConfig = configSection.Get<SqlDatabaseConfiugration>();

            if (useInMemoryDatabase)
            {
                services.AddDbContext<IDatabaseContext, DatabaseContext>(options =>
                    options.UseInMemoryDatabase("CleanArchitectureDb"));
            }
            else
            {
                services.AddDbContext<IDatabaseContext, DatabaseContext>(
                    options => options.UseSqlServer(databaseConfig.ConnectionString,
                    builder => builder.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));
            }

            var serviceBusConfiguration = configuration.GetSection("ServiceBus").Get<QueueClientConfiguration>();
            services.AddSingleton<QueueClientConfiguration>(serviceBusConfiguration);
            services.AddScoped<IQueueClient, ServiceBusQueueClient>();
            services.AddScoped<IUserTokenService, UserTokenService>();
            
            services.AddSingleton<SpotifyClientConfig>((c) => SpotifyClientConfig.CreateDefault());
            services.AddScoped<IStreamingService, SpotifyService>();

            return services;
        }
    }
}
