using HB.Framework.Common.Api;

namespace System
{
    public class ApiException : ClientException
    {
        public ApiErrorCode ErrorCode { get; set; }
        public int HttpCode { get; set; }

        public ApiException(string? message) : base(message)
        {
        }

        public ApiException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public ApiException()
        {
        }

        public ApiException(Exception? innnerException, ApiErrorCode apiError, int httpCode, string? message = null) : this(message, innnerException)
        {
            ErrorCode = apiError;
            HttpCode = httpCode;
        }

        public ApiException(ApiErrorCode apiError, int httpCode, string? message = null) : this(null, apiError, httpCode, message)
        {
        }
    }
}
