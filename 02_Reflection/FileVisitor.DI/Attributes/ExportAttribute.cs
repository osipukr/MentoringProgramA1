using System;

namespace FileVisitor.DI.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportAttribute : Attribute
    {
        /// <summary>
        ///     Ctor.
        /// </summary>
        public ExportAttribute()
        {

        }

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="typeToResolve"></param>
        public ExportAttribute(Type typeToResolve)
        {
            TypeToResolve = typeToResolve;
        }

        /// <summary>
        /// 
        /// </summary>
        public Type TypeToResolve { get; private set; }
    }
}