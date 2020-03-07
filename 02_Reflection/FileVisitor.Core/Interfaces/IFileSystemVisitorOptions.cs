using System;
using System.IO;

namespace FileVisitor.Core.Interfaces
{
    public interface IFileSystemVisitorOptions
    {
        /// <summary>
        ///     Filter by which files will be filtered.
        /// </summary>
        Func<FileSystemInfo, bool> SearchFilter { get; set; }

        /// <summary>
        ///     The search string to match against the names of directories.
        /// </summary>
        string SearchPattern { get; set; }

        /// <summary>
        ///     The specifies whether the search operation should
        ///     include only the current directory or all subdirectories.
        /// </summary>
        SearchOption SearchOption { get; set; }
    }
}