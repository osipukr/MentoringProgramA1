using System;

namespace Northwind.Server.BusinessLayer.Exceptions
{
    public class NorthwindException : Exception
    {
        public NorthwindException(ExceptionEventTypes exceptionEventType) : this(null, exceptionEventType)
        {
        }

        public NorthwindException(string message, ExceptionEventTypes exceptionEventType) : this(message, exceptionEventType, null)
        {
        }

        public NorthwindException(string message, ExceptionEventTypes exceptionEventType, Exception innerException) : base(message, innerException)
        {
            ExceptionEventType = exceptionEventType;
        }

        public ExceptionEventTypes ExceptionEventType { get; set; }
    }
}