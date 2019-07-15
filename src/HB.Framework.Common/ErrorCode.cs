namespace System
{
    public enum ErrorCode
    {
        FAILED = 0,
        OK = 1,
        NO_AUTHORITY = 4,
        NOT_AFTER_TODAY = 5,
        TEXT = 6,
        SMS_FAIL_SEND = 8,
        TOKEN_RERESH_ERROR = 10,
        API_TOKEN_VALIDATE_ERROR = 11,
        TOKEN_CREATE_ERROR = 12,
        DATA_OPERATION_FAILED = 14,
        DATA_NOT_WRITEABLE = 15,
        ARGUMENT_ERROR = 18,
        EXCEPTION_THROWN = 19,
        API_NOT_LOGIN_YET = 21,
        API_TOKEN_EXPIRED = 22,
        API_REQUEST_VALIDATE_ERROR = 23,
        API_TOKEN_DELETE_ERROR = 24,
        DATA_NO_SUCH = 25,
        DATA_RELATION_ERROR = 26,
    }

    public static class ErrorCodeExtensions
    {
        public static bool IsSuccess(this ErrorCode errorCode)
        {
            return errorCode == ErrorCode.OK;
        }
    }
}