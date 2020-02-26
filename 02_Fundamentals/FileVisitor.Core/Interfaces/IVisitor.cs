using System.Collections.Generic;
using System.IO;
using FileVisitor.Core.Models.Events;

namespace FileVisitor.Core.Interfaces
{
    public interface IVisitor
    {
        IEnumerable<FileSystemInfo> Visit();

        event VisitorStartEventHandler Started;
        event VisitorFinishEventHandler Finished;
    }
}