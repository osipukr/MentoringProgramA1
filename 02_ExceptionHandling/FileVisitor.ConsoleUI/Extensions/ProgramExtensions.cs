using System.IO;
using FileVisitor.ConsoleUI.Settings;
using FileVisitor.Core.Interfaces;
using FileVisitor.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileVisitor.ConsoleUI.Extensions
{
    public static class ProgramExtensions
    {
        public static ServiceCollection RegisterDependencies(this ServiceCollection services)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            var userSettings = configuration.GetSection(nameof(UserSettings)).Get<UserSettings>();

            services.AddSingleton(appSettings);
            services.AddSingleton(userSettings);

            services.AddScoped<IFileSystemVisitorOptions, FileSystemVisitorOptions>(_ =>
            {
                var options = new FileSystemVisitorOptions
                {
                    SearchFilter = info => info.Name.Contains("Core"),
                    SearchPattern = userSettings.SearchPattern,
                    SearchOption = userSettings.SearchOption
                };

                return options;
            });

            services.AddScoped<ILogger, ConsoleLogger>();
            services.AddScoped<IFileSystemVisitor, FileSystemVisitor>();
            services.AddScoped<Application>();

            return services;
        }
    }
}