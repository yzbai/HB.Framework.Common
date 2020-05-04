#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System
{
    public class FrameworkException : Exception
    {
        public virtual FrameworkExceptionType ExceptionType { get; set; }

        public FrameworkException(string? message) : base(message) { }

        public FrameworkException(string? message, Exception? innerException) : base(message, innerException) { }

        public FrameworkException() { }
    }

    public enum FrameworkExceptionType
    {
        Unkonwn = 0,
        Database = 1,
    }
}

#nullable restore