namespace System
{
    public enum ErrorCode
    {
        FAILED = 0,
        OK = 1,
        NOAUTHORITY = 4,
        NOTAFTERTODAY = 5,
        TEXT = 6,
        SMSFAILSEND = 8,
        TOKENRERESHERROR = 10,
        APITOKENVALIDATEERROR = 11,
        TOKENCREATEERROR = 12,
        DATAOPERATIONFAILED = 14,
        DATANOTWRITEABLE = 15,
        ARGUMENTERROR = 18,
        EXCEPTIONTHROWN = 19,
        APINOTLOGINYET = 21,
        APITOKENEXPIRED = 22,
        APIREQUESTVALIDATEERROR = 23,
        APITOKENDELETEERROR = 24,
        DATANOSUCH = 25,
        DATARELATIONERROR = 26,
        MODELVALIDATIONERROR = 27,
        APINEEDPUBLICRESOURCETOKEN = 28,
        APICAPTHAERROR = 29,
        APIINTERNALERROR = 30,
    }

    public static class ErrorCodeExtensions
    {
        public static bool IsSuccess(this ErrorCode errorCode)
        {
            return errorCode == ErrorCode.OK;
        }
    }
}