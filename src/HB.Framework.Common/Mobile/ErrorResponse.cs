
namespace HB.Framework.Common.Mobile
{
    public class ErrorResponse
    {
        public ErrorCode Code { get; set; }

        public string Message { get; set; }

        private ErrorResponse() { }

        public ErrorResponse(ErrorCode code, string message = null)
        {
            Code = code;
            Message = message;
        }
    }
}