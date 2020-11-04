using HB.Framework.Common.Api;

namespace System
{
    public class ApiException : ServerException
    {
        public ApiException(string? message) : base(message)
        {
        }

        public ApiException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public ApiException()
        {
        }

        public ApiException(ServerErrorCode errorCode, string? message) : base(errorCode, message)
        {
        }

        public ApiException(ServerErrorCode errorCode, string? message, Exception? innerException) : base(errorCode, message, innerException)
        {
        }

        public ApiException(ServerErrorCode errorCode) : base(errorCode)
        {
        }
    }
}
