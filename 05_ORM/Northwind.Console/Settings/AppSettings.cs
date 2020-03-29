using System.Globalization;

namespace Northwind.Console.Settings
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public CultureInfo Language { get; set; }
    }
}