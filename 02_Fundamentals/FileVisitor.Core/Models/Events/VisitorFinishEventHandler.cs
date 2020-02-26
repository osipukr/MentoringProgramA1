using FileVisitor.Core.Models.EventArgs;

namespace FileVisitor.Core.Models.Events
{
    public delegate void VisitorFinishEventHandler(object sender, VisitorFinishEventArgs e);
}