using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileVisitor.Core.Interfaces;
using FileVisitor.Core.Models.EventArgs;
using FileVisitor.Core.Models.Events;

namespace FileVisitor.Core.Services
{
    public class FileSystemVisitor : IVisitor
    {
        private readonly DirectoryInfo _startDirectory;
        private readonly FileSystemVisitorOptions _options;

        public FileSystemVisitor(string path) : this(path, new FileSystemVisitorOptions())
        {
        }

        public FileSystemVisitor(string path, FileSystemVisitorOptions options) : this(new DirectoryInfo(path), options)
        {
        }

        private FileSystemVisitor(DirectoryInfo startDirectory, FileSystemVisitorOptions options)
        {
            _startDirectory = startDirectory ?? throw new ArgumentNullException(nameof(startDirectory));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public IEnumerable<FileSystemInfo> Visit()
        {
            Started?.Invoke(this, new VisitorStartEventArgs());

            var files = _startDirectory
                .GetFileSystemInfos("*.*", SearchOption.AllDirectories)
                .Where(_options.Filter);

            Finished?.Invoke(this, new VisitorFinishEventArgs());

            return files;
        }

        public event VisitorStartEventHandler Started;
        public event VisitorFinishEventHandler Finished;
    }
}