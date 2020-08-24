#nullable enable

using System.Diagnostics.CodeAnalysis;
using HB.Framework.Common.Api;

namespace HB.Framework.Common.Api
{
    public class ApiResponse
    {
        public int HttpCode { get; private set; }

        public string? Message { get; private set; }

        public ApiErrorCode ErrCode { get; private set; } = ApiErrorCode.OK;

        public ApiResponse(int httpCode)
        {
            HttpCode = httpCode;
        }

        public ApiResponse(int httpCode, string? message, ApiErrorCode errorCode) : this(httpCode)
        {
            Message = message;
            ErrCode = errorCode;
        }

    }

    public class ApiResponse<T> : ApiResponse where T : class
    {
        public T? Data { get; set; }

        public ApiResponse(T? data, int httpCode) : base(httpCode)
        {
            Data = data;
        }

        public ApiResponse(int httpCode, string? message, ApiErrorCode errorCode) : base(httpCode, message, errorCode) { }

        public bool IsSuccessful { get => HttpCode >= 200 && HttpCode <= 299; }
    }
}
