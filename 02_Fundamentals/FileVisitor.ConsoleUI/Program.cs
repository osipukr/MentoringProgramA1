using FileVisitor.ConsoleUI.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace FileVisitor.ConsoleUI
{
    public sealed class Program
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