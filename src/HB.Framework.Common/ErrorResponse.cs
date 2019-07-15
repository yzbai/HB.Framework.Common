
namespace System
{
    public class ErrorResponse
    {
        public ErrorCode Code { get; set; }

        public string Message { get; set; }

        private ErrorResponse() { }

        public ErrorResponse(ErrorCode code)
        {
            Code = code;
        }
    }
}