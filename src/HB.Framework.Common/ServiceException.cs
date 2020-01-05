using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public class ServiceException : Exception
    {
        public string ServiceName { get; set; }

        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ServiceException()
        {
        }
    }
}
