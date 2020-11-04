using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public enum ServerErrorCode
    {
        //200
        OK = 1,

        #region API

        ApiError = 19,

        //401
        ApiNoAuthority = 14,
        ApiTokenRefresherError = 10,
        ApiTokenExpired = 122,
        ApiKeyUnAuthenticated = 146,

        //400
        ApiModelValidationError = 127,
        ApiPublicResourceTokenNeeded = 128,
        ApiPublicResourceTokenError = 151,
        ApiCapthaError = 129,

        ApiHttpsRequired = 147,
        ApiUploadEmptyFile = 148,
        ApiUploadOverSize = 149,
        ApiUploadWrongType = 150,
        ApiSmsCodeInvalid = 152,

        //502
        ApiNoInternet = 145,

        #endregion

        #region Database

        DatabaseError = 233,


        /// <summary>
        /// 错误：没有找到
        /// </summary>
        DatabaseNotFound = 232,

        /// <summary>
        /// 错误：不可写
        /// </summary>
        DatabaseNotWriteable = 23,

        /// <summary>
        /// 错误：Scalar查询，返回多个
        /// </summary>
        DatabaseFoundTooMuch = 24,

        DatabaseArgumentNotValid = 25,
        DatabaseNotMatch = 26,

        DatabaseTransactionError = 27,
        DatabaseTableCreateError = 28,
        DatabaseMigrateError = 29,
        DatabaseNotATableModel = 210,
        DatabaseTransactionConnectionIsNull = 211,
        DatabaseExecuterError = 241,

        #endregion

        #region KVStore

        KVStoreError = 391,

        KVStoreNotFound = 32,
        KVStoreExistAlready = 33,
        KVStoreVersionNotMatched = 34,
        KVStoreNotValid = 35,
        KVStoreNoEntitySchemaFound = 37,
        KVStoreRedisTimeout = 38,
        KVStoreRedisConnectionFailed = 390,

        #endregion

        #region Identity

        IdentityError = 40,
        IdentityNotFound = 42,
        IdentityAlreadyExists = 43,
        IdentityArgumentError = 44,
        IdentityMobileAlreadyTaken = 45,
        IdentityLoginNameAlreadyTaken = 46,
        IdentityEmailAlreadyTaken = 47,

        #endregion

        #region Authorization

        AuthorizationError = 51,
        AuthorizationLogoffOtherClientFailed = 52,
        AuthorizationNewUserCreateFailed = 53,
        AuthorizationNewUserCreateFailedMobileAlreadyTaken = 54,
        AuthorizationNewUserCreateFailedEmailAlreadyTaken = 55,
        AuthorizationNewUserCreateFailedLoginNameAlreadyTaken = 56,
        AuthorizationLockedOut = 57,
        AuthorizationTwoFactorRequired = 58,
        AuthorizationMobileNotConfirmed = 59,
        AuthorizationEmailNotConfirmed = 510,
        AuthorizationOverMaxFailedCount = 511,
        AuthorizationPasswordWrong = 513,
        AuthorizationAuthtokenCreatedFailed = 514,
        AuthorizationArgumentError = 515,
        AuthorizationNotFound = 517,
        AuthorizationTooFrequent = 518,
        AuthorizationInvalideAccessToken = 519,
        AuthorizationInvalideUserGuid = 520,
        AuthorizationNoTokenInStore = 521,
        AuthorizationUserSecurityStampChanged = 522,
        AuthorizationUpdateSignInTokenError = 523,
        AuthorizationInvalideDeviceId = 524,
        AuthorizationRefreshTokenExpired = 525,

        #endregion
    }

    public static class ErrorCodeExtensions
    {
        public static bool IsSuccessful(this ServerErrorCode errorCode)
        {
            return errorCode == ServerErrorCode.OK;
        }
    }
}
