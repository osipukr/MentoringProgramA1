namespace ProjectName.Core.Interfaces
{
    /// <summary>
    ///     Represents a file manager interface.
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        ///     Сhecks if the file exists.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool IsExist(string path);

        /// <summary>
        ///     Gets the contents of the file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string GetContent(string path);
    }
}