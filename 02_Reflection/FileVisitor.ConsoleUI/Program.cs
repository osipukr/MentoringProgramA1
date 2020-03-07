using FileVisitor.ConsoleUI.Extensions;
using FileVisitor.DI.Services;

namespace FileVisitor.ConsoleUI
{
    public sealed class Program
    {
        private static void Main()
        {
            new Application(
                new Container()
                    .RegisterDependencies())
                .Run();
        }
    }
}