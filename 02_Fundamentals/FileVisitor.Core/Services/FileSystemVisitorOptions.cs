using System;
using System.IO;

namespace FileVisitor.Core.Services
{
    /// <summary>
    ///     Represent of file system visitor options.
    /// </summary>
    public sealed class FileSystemVisitorOptions
    {
        public FileSystemVisitorOptions()
        {
            SearchFilter = null;
            SearchPattern = "*.*";
            SearchOption = SearchOption.AllDirectories;
        }

        /// <summary>
        ///     Filter by which files will be filtered.
        /// </summary>
        public Func<FileSystemInfo, bool> SearchFilter { get; set; }

        /// <summary>
        ///     The search string to match against the names of directories.
        /// </summary>
        public string SearchPattern { get; set; }

        /// <summary>
        ///     The specifies whether the search operation should
        ///     include only the current directory or all subdirectories.
        /// </summary>
        public SearchOption SearchOption { get; set; }
    }
}