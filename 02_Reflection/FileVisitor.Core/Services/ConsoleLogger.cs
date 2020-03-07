using System;
using FileVisitor.Core.Interfaces;

namespace FileVisitor.Core.Services
{
    /// <summary>
    ///     Represents a console logger implementation.
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        public void LogMessage(string message)
        {
            Console.WriteLine($"[Log]: | {message} |");
        }
    }
}