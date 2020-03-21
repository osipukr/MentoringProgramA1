using System;

namespace Northwind.DAL.Exceptions
{
    /// <summary>
    /// Custom repository exception.
    /// </summary>
    public class RepositoryException : Exception
    {
        public RepositoryException() : this(null)
        {
        }

        public RepositoryException(string message) : this(message, null)
        {
        }

        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}