#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System
{
    public class ServiceException : Exception
    {
        [MaybeNull, DisallowNull] public string? ServiceName { get; set; }

        public ServiceException(string message) : base(message) { }

        public ServiceException(string message, Exception innerException) : base(message, innerException) { }

        public ServiceException() { }

        public ServiceException(string serviceName, string message) : this(message)
        {
            ServiceName = serviceName;
        }

        public ServiceException(string serviceName, string message, Exception innerException) : this(message, innerException)
        {
            ServiceName = serviceName;
        }
    }
}

#nullable restore