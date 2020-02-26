using System;
using System.Runtime.Serialization;

namespace ProjectName.Core.Exceptions
{
    /// <summary>
    ///     Custom file reader exception.
    /// </summary>
    public class FileReadException : Exception
    {
        protected FileReadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="filePath"></param>
        public FileReadException(string filePath) : this(null, filePath)
        {
        }

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="filePath"></param>
        public FileReadException(string message, string filePath) : this(message, filePath, null)
        {
        }

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="filePath"></param>
        /// <param name="innerException"></param>
        public FileReadException(string message, string filePath, Exception innerException) : base(message, innerException)
        {
            FilePath = filePath;
        }

        public string FilePath { get; protected set; }
    }
}