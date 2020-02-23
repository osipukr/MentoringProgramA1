using Microsoft.Extensions.DependencyInjection;

namespace ProjectName.ConsoleUI
{
    public class Program
    {
        private static void Main()
        {
            new ServiceCollection()
                .RegisterDependencies()
                .BuildServiceProvider()
                .GetService<Application>()
                .Run();
        }
    }
}