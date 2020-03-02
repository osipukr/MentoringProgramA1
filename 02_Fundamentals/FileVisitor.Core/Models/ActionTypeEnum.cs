namespace FileVisitor.Core.Models
{
    /// <summary>
    ///     The list of search actions.
    /// </summary>
    public enum ActionTypeEnum
    {
        /// <summary>
        ///     Continue searching.
        /// </summary>
        ContinueSearch = 1,

        /// <summary>
        ///     Skip an item from the search.
        /// </summary>
        SkipElement = 2,

        /// <summary>
        ///     Stop searching.
        /// </summary>
        StopSearch = 3
    }
}