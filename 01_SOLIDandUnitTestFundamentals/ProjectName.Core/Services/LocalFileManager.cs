using System;
using System.IO;
using ProjectName.Core.Exceptions;
using ProjectName.Core.Interfaces;

namespace ProjectName.Core.Services
{
    /// <summary>
    ///     Represents a file manager implementation.
    /// </summary>
    public class LocalFileManager : IFileManager
    {
        private readonly ILogger _logger;

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="logger"></param>
        public LocalFileManager(ILogger logger)
        {
            _logger = logger;
        }

        public bool IsExist(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            var result = File.Exists(path);

            _logger.LogMessage($"Method '{nameof(IsExist)}' is executed with the result: {result}.");

            return result;
        }

        public string GetContent(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            string result;

            try
            {
                result = File.ReadAllText(path);

                _logger.LogMessage($"Method '{nameof(GetContent)}' is executed.");
            }
            catch (Exception ex)
            {
                throw new FileReadException(ex.Message, path, ex);
            }

            return result;
        }
    }
}