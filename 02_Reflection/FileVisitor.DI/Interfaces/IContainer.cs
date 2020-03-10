namespace FileVisitor.DI.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTypeToResolve"></typeparam>
        /// <typeparam name="TTargetType"></typeparam>
        /// <param name="isSingleton"></param>
        void Register<TTypeToResolve, TTargetType>(bool isSingleton = false);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTypeToResolve"></typeparam>
        /// <param name="isSingleton"></param>
        void Register<TTypeToResolve>(bool isSingleton = false);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TTypeToResolve"></typeparam>
        /// <returns></returns>
        TTypeToResolve Resolve<TTypeToResolve>();
    }
}