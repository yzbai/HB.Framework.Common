﻿#nullable enable

namespace HB.Framework.Common.Api
{
    public enum ApiErrorCode
    {
        FAILED = 0,
        OK = 1,
        NOAUTHORITY = 4,
        TokenRefresherError = 10,
        NOTAFTERTODAY = 5,
        TEXT = 6,
        SmsCodeInvalid = 8,
        APITOKENVALIDATEERROR = 11,
        TokenCreateError = 12,
        DATAOPERATIONFAILED = 14,
        DATANOTWRITEABLE = 15,
        ARGUMENTERROR = 18,
        EXCEPTIONTHROWN = 19,
        //ApiNotLoginYet = 21,
        ApiTokenExpired = 22,
        ApiRequestValidateError = 23,
        APITOKENDELETEERROR = 24,
        DATANOSUCH = 25,
        DATARELATIONERROR = 26,
        MODELVALIDATIONERROR = 27,
        PublicResourceTokenNeeded = 28,
        APICAPTHAERROR = 29,
        ApiUtilsError = 30,
        APIFAILED = 31,
        EndpointNotFound = 32,
        HTTPSREQUIRED = 33,
        DEVELOPMENTONLY = 34,
        APIKEYUNAUTHENTICATED = 35,
        PUBLICRESOURCETOKENERROR = 36,
        FileUploadNeeded = 37,
        FileUploadMultipartFormDataNeeded = 38,
        FileUploadTypeNotMatch = 39,
        FileUploadOverSize = 40,
        FileUploadError = 41,
        UserGuidAbsent = 42,
        UnKownError = 43,
        NullResponseDataReturn = 44,
    }

    public static class ErrorCodeExtensions
    {
        public static bool IsSuccess(this ApiErrorCode errorCode)
        {
            return errorCode == ApiErrorCode.OK;
        }
    }
}