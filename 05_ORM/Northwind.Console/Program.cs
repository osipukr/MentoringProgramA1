using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Console.Extensions;

namespace Northwind.Console
{
    public class Program
    {
        private static async Task Main()
        {
            await new ServiceCollection()
                .RegisterDependencies()
                .BuildServiceProvider()
                .GetRequiredService<Application>()
                .RunAsync();
        }
    }
}