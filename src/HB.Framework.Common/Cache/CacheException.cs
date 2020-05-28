using System;

namespace HB.Framework.Common.Cache
{
    public class CacheException : FrameworkException
    {
        public override FrameworkExceptionType ExceptionType { get => FrameworkExceptionType.Cache; }

        public CacheException(string message) : base(message)
        {
        }

        public CacheException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CacheException()
        {
        }
    }
}
