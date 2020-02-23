using Microsoft.Extensions.DependencyInjection;
using ProjectName.Core.Interfaces;
using ProjectName.Core.Services;

namespace ProjectName.ConsoleUI
{
    public static class ProgramExtensions
    {
        public static ServiceCollection RegisterDependencies(this ServiceCollection services)
        {
            services.AddScoped<ILogger, ConsoleLogger>();
            services.AddScoped<IFileManager, LocalFileManager>();
            services.AddScoped<Application>();

            return services;
        }
    }
}