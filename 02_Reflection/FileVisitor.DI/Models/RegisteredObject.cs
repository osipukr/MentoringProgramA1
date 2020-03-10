using System;

namespace FileVisitor.DI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class RegisteredObject
    {
        /// <summary>
        /// 
        /// </summary>
        public Type TypeToResolve { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Type TargetType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSingleton { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object SingletonInstance { get; set; }
    }
}