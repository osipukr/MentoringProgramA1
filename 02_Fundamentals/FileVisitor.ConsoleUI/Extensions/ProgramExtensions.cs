using System.IO;
using FileVisitor.ConsoleUI.Settings;
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

            services.AddScoped<Application>();

            return services;
        }
    }
}