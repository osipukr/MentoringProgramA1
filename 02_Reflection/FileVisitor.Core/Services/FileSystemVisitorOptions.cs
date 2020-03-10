using System;
using System.IO;
using FileVisitor.Core.Interfaces;

namespace FileVisitor.Core.Services
{
    /// <summary>
    ///     Represent of file system visitor options.
    /// </summary>
    public class FileSystemVisitorOptions : IFileSystemVisitorOptions
    {
        public Func<FileSystemInfo, bool> SearchFilter { get; set; } = null;

        public string SearchPattern { get; set; } = "*.*";

        public SearchOption SearchOption { get; set; } = SearchOption.AllDirectories;
    }
}