using System;
using FileVisitor.Core.Interfaces;

namespace FileVisitor.Core.Exceptions
{
    /// <summary>
    ///     Invalid visitor option exception.
    /// </summary>
    public class InvalidVisitorOptionException : Exception
    {
        /// <summary>
        ///     Ctor.
        /// </summary>
        public InvalidVisitorOptionException()
        {
        }

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="options"></param>
        public InvalidVisitorOptionException(IFileSystemVisitorOptions options)
            : this(null, options)
        {
        }

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="options"></param>
        public InvalidVisitorOptionException(string message, IFileSystemVisitorOptions options)
            : this(message, options, null)
        {
        }

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="options"></param>
        /// <param name="innerException"></param>
        public InvalidVisitorOptionException(string message, IFileSystemVisitorOptions options, Exception innerException)
            : base(message, innerException)
        {
            Options = options;
        }

        /// <summary>
        ///     Gets file system visitor options.
        /// </summary>
        public IFileSystemVisitorOptions Options { get; }
    }
}