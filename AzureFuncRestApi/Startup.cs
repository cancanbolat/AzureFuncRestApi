using AzureFuncRestApi.Data;
using AzureFuncRestApi.Interfaces;
using AzureFuncRestApi.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(AzureFuncRestApi.Startup))]
namespace AzureFuncRestApi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

            builder.Services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(connectionString, op => op.EnableRetryOnFailure());
            });

            builder.Services.AddScoped<IProductService, ProductService>();
        }
    }
}
