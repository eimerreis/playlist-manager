using Api.Models.Configuration;
using Api.Services;
using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string CorsPolicyName = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddCors((options) =>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                {
                    builder.WithOrigins(Configuration.GetSection("AllowedOrigins").Get<string[]>());
                });
            });

            services.AddHttpContextAccessor();
            services.AddAuthentication("Spotify").AddScheme<SpotifyAuthenticationOptions, SpotifyAuthenticationHandler>("Spotify", null);

            var spotifyConfig = Configuration.GetSection("Spotify").Get<SpotifyConfiguration>();
            services.AddSingleton<SpotifyConfiguration>(spotifyConfig);

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddApplication();

            //services.AddDbContext<IDatabaseContext, DatabaseContext>(options => options.UseCosmos(cosmosDbConfig.Endpoint, cosmosDbConfig.AccountKey, cosmosDbConfig.DatabaseName));
            services.AddInfrastructure(Configuration, false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(CorsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
