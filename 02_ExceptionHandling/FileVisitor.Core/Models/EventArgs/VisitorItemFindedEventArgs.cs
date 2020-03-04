using System.IO;

namespace FileVisitor.Core.Models.EventArgs
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class VisitorItemFindedEventArgs<TItem> : System.EventArgs where TItem : FileSystemInfo
    {
        /// <summary>
        ///     The element is found.
        /// </summary>
        public TItem FindedItem { get; set; }

        /// <summary>
        ///     The action type is found.
        /// </summary>
        public ActionTypeEnum ActionType { get; set; }
    }
}