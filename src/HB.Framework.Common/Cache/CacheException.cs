using System;

namespace HB.Framework.Common.Cache
{
    public class CacheException : ServerException
    {

        public CacheException(string? message) : base(message)
        {
        }

        public CacheException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public CacheException()
        {
        }

        public CacheException(ServerErrorCode errorCode, string? message) : base(errorCode, message)
        {
        }

        public CacheException(ServerErrorCode errorCode, string? message, Exception? innerException) : base(errorCode, message, innerException)
        {
        }

        public CacheException(ServerErrorCode errorCode) : base(errorCode)
        {
        }
    }
}
