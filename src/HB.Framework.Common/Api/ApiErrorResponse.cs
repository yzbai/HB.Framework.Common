
namespace HB.Framework.Common.Api
{
    public class ApiErrorResponse
    {
        public ApiError Code { get; set; }

        public string Message { get; set; }

        private ApiErrorResponse() { }

        public ApiErrorResponse(ApiError code, string message = null)
        {
            Code = code;
            Message = message;
        }
    }
}