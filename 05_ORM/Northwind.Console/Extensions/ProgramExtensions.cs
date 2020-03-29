using System;
using System.IO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Northwind.BL.Interfaces;
using Northwind.BL.Services;
using Northwind.Console.Settings;
using Northwind.DAL.Abstractions.Interfaces;
using Northwind.DAL.Contexts;
using Northwind.DAL.Services;

namespace Northwind.Console.Extensions
{
    public static class ProgramExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            var environment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Program>(optional: true)
                .AddEnvironmentVariables()
                .Build();

            services.AddLogging(options =>
            {
                options.ClearProviders();
                options.AddConfiguration(configuration);
                options.AddConsole();
            });

            services.AddAutoMapper(typeof(Program));

            var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

            services.AddSingleton(appSettings);

            #region Code First DI

            services.AddDbContext<NorthwindContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
                options.UseLazyLoadingProxies();
            });

            services.AddScoped<IUnitOfWork<NorthwindContext>, UnitOfWork>();
            services.AddScoped<IOrderService, OrderService>();

            #endregion

            #region Database First DI



            #endregion

            services.AddSingleton<Application>();

            return services;
        }
    }
}