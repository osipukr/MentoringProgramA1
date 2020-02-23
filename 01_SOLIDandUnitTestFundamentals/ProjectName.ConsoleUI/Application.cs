using System;
using System.IO;
using ProjectName.Core.Interfaces;

namespace ProjectName.ConsoleUI
{
    /// <summary>
    /// 
    /// </summary>
    public class Application
    {
        private readonly IFileManager _fileManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileManager"></param>
        public Application(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Run()
        {
            FileManagerWorker();
        }

        private void FileManagerWorker()
        {
            var path = Path.Combine("Resources", "TextResource.txt");

            if (_fileManager.IsExist(path))
            {
                var content = _fileManager.GetContent(path);

                Console.WriteLine(content);
            }
        }
    }
}