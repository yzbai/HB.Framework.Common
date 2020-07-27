#nullable enable

using HB.Framework.Common.Api;

namespace HB.Framework.Common.Api
{

    public class ApiResponse : ApiResponse<ApiResponseData>
    {
        public ApiResponse(ApiResponseData? data, int httpCode) : base(data, httpCode) { }

        public ApiResponse(int httpCode, string? message, ApiError errorCode) : base(httpCode, message, errorCode) { }

    }

    public class ApiResponse<T> where T : ApiResponseData
    {
        public int HttpCode { get; private set; }

        public string? Message { get; private set; }

        public ApiError ErrCode { get; private set; } = ApiError.OK;

        public T? Data { get; set; }

        public ApiResponse(T? data, int httpCode)
        {
            Data = data;
            HttpCode = httpCode;
        }

        public ApiResponse(int httpCode, string? message, ApiError errorCode)
        {
            HttpCode = httpCode;
            Message = message;
            ErrCode = errorCode;
        }

        public bool IsSuccessful()
        {
            return HttpCode >= 200 && HttpCode <= 299;
        }

        public static implicit operator ApiResponse(ApiResponse<T> t)
        {
            ApiResponse rt = new ApiResponse(t.HttpCode, t.Message, t.ErrCode)
            {
                Data = t.Data
            };

            return rt;
        }

        public static implicit operator ApiResponse<T>(ApiResponse v)
        {
            ApiResponse<T> rt = new ApiResponse<T>(v.HttpCode, v.Message, v.ErrCode);

            if (v.Data != null)
            {
                rt.Data = (T)v.Data;
            }

            return rt;
        }

        public ApiResponse ToApiResponse()
        {
            ApiResponse rt = new ApiResponse(HttpCode, Message, ErrCode)
            {
                Data = Data
            };

            return rt;
        }
    }
}
