using Microsoft.Extensions.DependencyInjection;

namespace FileVisitor.ConsoleUI
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