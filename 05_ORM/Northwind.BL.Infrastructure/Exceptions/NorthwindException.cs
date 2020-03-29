using System;

namespace Northwind.BL.Infrastructure.Exceptions
{
    public class NorthwindException : Exception
    {
        public NorthwindException() : this(null)
        {
        }

        public NorthwindException(string message) : this(message, null)
        {
        }

        public NorthwindException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}