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

        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ApiException()
        {
        }

        public ApiException(ApiErrorCode apiError) : this()
        {
            ErrorCode = apiError;
        }

        public ApiException(ApiErrorCode apiError, string? message) : this(message)
        {
            ErrorCode = apiError;
        }

        public ApiException(Exception innnerException, ApiErrorCode apiError, string message) : this(message, innnerException)
        {
            ErrorCode = apiError;
        }

        public ApiException(ApiErrorCode apiError, string? message, int httpCode) : this(apiError, message)
        {
            HttpCode = httpCode;
        }
    }
}
