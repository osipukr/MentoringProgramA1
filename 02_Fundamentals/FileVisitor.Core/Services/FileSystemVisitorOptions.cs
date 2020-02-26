using System;
using System.IO;

namespace FileVisitor.Core.Services
{
    public class FileSystemVisitorOptions
    {
        public FileSystemVisitorOptions()
        {
            
        }

        public Func<FileSystemInfo, bool> Filter { get; set; } = _ => true;
    }
}