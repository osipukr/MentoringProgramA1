namespace FileVisitor.Core.Interfaces
{
    /// <summary>
    ///     Represents a logger interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        ///     Writes log message.
        /// </summary>
        /// <param name="message"></param>
        void LogMessage(string message);
    }
}