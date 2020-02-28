using System;
using System.Collections.Generic;
using System.IO;
using FileVisitor.Core.Interfaces;
using FileVisitor.Core.Models;
using FileVisitor.Core.Models.EventArgs;

namespace FileVisitor.Core.Services
{
    /// <summary>
    ///     Represent of file system visitor interface.
    /// </summary>
    public sealed class FileSystemVisitor : IFileSystemVisitor
    {
        private readonly DirectoryInfo _startDirectory;
        private readonly FileSystemVisitorOptions _options;

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="path"></param>
        public FileSystemVisitor(string path) : this(path, new FileSystemVisitorOptions())
        {
        }

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="options"></param>
        public FileSystemVisitor(string path, FileSystemVisitorOptions options) : this(new DirectoryInfo(path), options)
        {
        }

        private FileSystemVisitor(DirectoryInfo startDirectory, FileSystemVisitorOptions options)
        {
            _startDirectory = startDirectory ?? throw new ArgumentNullException(nameof(startDirectory));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        ///     Gets a list of found files and directories.
        /// </summary>
        public IEnumerable<FileSystemInfo> Visit()
        {
            if (!_startDirectory.Exists)
            {
                throw new DirectoryNotFoundException($"Directory {_startDirectory} not found.");
            }

            OnEvent(Started, new VisitorStartEventArgs());

            foreach (var file in BypassFileSystem(_startDirectory, _options, ActionTypeEnum.ContinueSearch))
            {
                yield return file;
            }

            OnEvent(Finished, new VisitorFinishEventArgs());
        }

        public event EventHandler<VisitorStartEventArgs> Started;
        public event EventHandler<VisitorFinishEventArgs> Finished;
        public event EventHandler<VisitorItemFindedEventArgs<FileInfo>> FileFinded;
        public event EventHandler<VisitorItemFindedEventArgs<FileInfo>> FilteredFileFinded;
        public event EventHandler<VisitorItemFindedEventArgs<DirectoryInfo>> DirectoryFinded;
        public event EventHandler<VisitorItemFindedEventArgs<DirectoryInfo>> FilteredDirectoryFinded;

        private IEnumerable<FileSystemInfo> BypassFileSystem(
            DirectoryInfo directory,
            FileSystemVisitorOptions options,
            ActionTypeEnum currentAction)
        {
            var files = directory.EnumerateFileSystemInfos(options.SearchPattern, options.SearchOption);

            foreach (var file in files)
            {
                if (file is FileInfo fileInfo)
                {
                    currentAction = ProcessItemFinded(fileInfo, options.SearchFilter, FileFinded, FilteredFileFinded);
                }

                if (file is DirectoryInfo directoryInfo)
                {
                    currentAction = ProcessItemFinded(directoryInfo, options.SearchFilter, DirectoryFinded, FilteredDirectoryFinded);
                }

                if (currentAction == ActionTypeEnum.StopSearch)
                {
                    yield break;
                }

                if (currentAction == ActionTypeEnum.SkipElement)
                {
                    continue;
                }

                yield return file;
            }
        }

        private ActionTypeEnum ProcessItemFinded<TItem>(
            TItem file,
            Func<TItem, bool> filter,
            EventHandler<VisitorItemFindedEventArgs<TItem>> itemFinded,
            EventHandler<VisitorItemFindedEventArgs<TItem>> filteredItemFinded)
            where TItem : FileSystemInfo
        {
            var args = new VisitorItemFindedEventArgs<TItem>
            {
                FindedItem = file,
                ActionType = ActionTypeEnum.ContinueSearch
            };

            OnEvent(itemFinded, args);

            if (args.ActionType != ActionTypeEnum.ContinueSearch || filter == null)
            {
                return args.ActionType;
            }

            if (!filter(file))
            {
                return ActionTypeEnum.SkipElement;
            }

            OnEvent(filteredItemFinded, args);

            return args.ActionType;
        }

        private void OnEvent<TArgs>(EventHandler<TArgs> eventHandler, TArgs args)
        {
            eventHandler?.Invoke(this, args);
        }
    }
}