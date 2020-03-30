using System.Globalization;
using Northwind.Console.Settings;

namespace Northwind.Console.Extensions
{
    public static class AppSettingsExtensions
    {
        public static AppSettings ConfigureApplication(this AppSettings settings)
        {
            CultureInfo.CurrentCulture = settings.Language;
            CultureInfo.CurrentUICulture = settings.Language;

            return settings;
        }
    }
}