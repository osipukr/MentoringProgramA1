using System.IO;
using FileVisitor.ConsoleUI.Settings;
using FileVisitor.Core.Interfaces;
using FileVisitor.Core.Services;
using FileVisitor.DI.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FileVisitor.ConsoleUI.Extensions
{
    public static class ContainerExtensions
    {
        public static IContainer RegisterDependencies(this IContainer container)
        {
            container.Register<AppSettings>(isSingleton: true);
            container.Register<UserSettings>(isSingleton: true);
            container.Register<IFileSystemVisitorOptions, FileSystemVisitorOptions>(isSingleton: true);
            container.Register<ILogger, ConsoleLogger>();
            container.Register<IFileSystemVisitor, FileSystemVisitor>();

            #region Configuration

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            #endregion

            #region AppSettings

            var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            var newAppSettings = container.Resolve<AppSettings>();

            newAppSettings.Language = appSettings.Language;

            #endregion

            #region UserSettings

            var userSettings = configuration.GetSection(nameof(UserSettings)).Get<UserSettings>();
            var newUserSettings = container.Resolve<UserSettings>();

            newUserSettings.DirectoryPath = userSettings.DirectoryPath;
            newUserSettings.SearchPattern = userSettings.SearchPattern;
            newUserSettings.SearchOption = userSettings.SearchOption;

            #endregion

            #region IFileSystemVisitorOptions

            var options = container.Resolve<IFileSystemVisitorOptions>();

            options.SearchFilter = info => info.Name.Contains("Core");
            options.SearchPattern = userSettings.SearchPattern;
            options.SearchOption = userSettings.SearchOption;

            #endregion

            return container;
        }
    }
}