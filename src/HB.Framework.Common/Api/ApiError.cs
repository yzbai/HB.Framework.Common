namespace HB.Framework.Common.Api
{
    public enum ApiError
    {
        FAILED = 0,
        OK = 1,
        NOAUTHORITY = 4,
        NOTAFTERTODAY = 5,
        TEXT = 6,
        SMSFAILSEND = 8,
        TokenRefresherError = 10,
        APITOKENVALIDATEERROR = 11,
        TokenCreateError = 12,
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
        APIFAILED = 31,
    }

    public static class ErrorCodeExtensions
    {
        public static bool IsSuccess(this ApiError errorCode)
        {
            return errorCode == ApiError.OK;
        }
    }
}