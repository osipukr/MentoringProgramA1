using FileVisitor.Core.Interfaces;
using FileVisitor.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FileVisitor.ConsoleUI
{
    public static class ProgramExtensions
    {
        public static ServiceCollection RegisterDependencies(this ServiceCollection services)
        {
            services.AddScoped<Application>();

            return services;
        }
    }
}