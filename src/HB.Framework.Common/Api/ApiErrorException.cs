using HB.Framework.Common.Api;

namespace System
{
    public class ApiErrorException : Exception
    {
        public ApiError ApiError { get; set; }

        public ApiErrorException(string message) : base(message)
        {
        }

        public ApiErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ApiErrorException()
        {
        }

        public ApiErrorException(ApiError apiError) : this()
        {
            ApiError = apiError;
        }

        public ApiErrorException(ApiError apiError, string message) : this(message)
        {
            ApiError = apiError;
        }
    }
}
