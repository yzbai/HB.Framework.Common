#nullable enable


namespace System
{
    public class ServerException : Exception
    {
        public ServerException(ServerErrorCode errorCode, string? message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ServerException(ServerErrorCode errorCode, string? message, Exception? innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        public ServerException(ServerErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public ServerErrorCode ErrorCode { get; set; }

        public ServerException()
        {
        }

        public ServerException(string? message) : base(message)
        {
        }

        public ServerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

#nullable restore