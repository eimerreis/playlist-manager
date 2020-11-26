using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Infrastructure.Persistence;
using ManagementJobFunction.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(ManagementJobFunction.Startup))]

namespace ManagementJobFunction
{
    public class Startup: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(configuration, false);
            builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
        }
    }
}
