using System;
using ProjectName.Core.Interfaces;

namespace ProjectName.Core.Services
{
    /// <summary>
    ///     Represents a console logger implementation.
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        ///     Ctor.
        /// </summary>
        public ConsoleLogger()
        {
        }

        public void LogMessage(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            Console.WriteLine($"[Log]: | {message} |");
        }
    }
}