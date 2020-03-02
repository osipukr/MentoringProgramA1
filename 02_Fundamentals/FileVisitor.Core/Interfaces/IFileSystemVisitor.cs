using System;
using System.Collections.Generic;
using System.IO;
using FileVisitor.Core.Models.EventArgs;

namespace FileVisitor.Core.Interfaces
{
    public interface IFileSystemVisitor
    {
        /// <summary>
        ///     Gets a list of visited files.
        /// </summary>
        IEnumerable<FileSystemInfo> Visit(string directoryPath);

        /// <summary>
        ///     Gets a file system visitor options.
        /// </summary>
        IFileSystemVisitorOptions Options { get; }

        /// <summary>
        ///     Event that is triggered when the search starts.
        /// </summary>
        event EventHandler<VisitorStartEventArgs> Started;

        /// <summary>
        ///     Event that is triggered when the search ends.
        /// </summary>
        event EventHandler<VisitorFinishEventArgs> Finished;

        /// <summary>
        ///     Event that is triggered when a file is found.
        /// </summary>
        event EventHandler<VisitorItemFindedEventArgs<FileInfo>> FileFinded;

        /// <summary>
        ///     Event that is triggered when a file is found by a filter.
        /// </summary>
        event EventHandler<VisitorItemFindedEventArgs<FileInfo>> FilteredFileFinded;

        /// <summary>
        ///     Event that is triggered when a directory is found.
        /// </summary>
        event EventHandler<VisitorItemFindedEventArgs<DirectoryInfo>> DirectoryFinded;

        /// <summary>
        ///     Event that is triggered when a directory is found by a filter.
        /// </summary>
        event EventHandler<VisitorItemFindedEventArgs<DirectoryInfo>> FilteredDirectoryFinded;
    }
}