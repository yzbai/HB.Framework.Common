using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;
using System.Collections;

namespace System
{
    public static class LoggerExtensions
    {
        public static void LogException(this ILogger logger, Exception exception, string message =null, LogLevel logLevel = LogLevel.Error, [CallerMemberName]string caller = null)
        {
            ThrowIf.Null(logger, nameof(logger));
            //ThrowIf.Null(exception, nameof(exception));

            if (exception == null)
            {
                return;
            }

            ////TODO: Collection was modified; enumeration operation may not execute.解决这个问题

            //StringBuilder stringBuilder = new StringBuilder();


            //foreach (string key in exception.Data.Keys)
            //{
            //    stringBuilder.Append($"{key}:{exception.Data[key].ToString()}, ");
            //}

            //if (exception.InnerException != null)
            //{
            //    foreach (string key in exception.InnerException.Data.Keys)
            //    {
            //        stringBuilder.Append($"{key}:{exception.InnerException.Data[key].ToString()}, ");
            //    }
            //}

            logger.Log(logLevel, exception, $"Caller : {caller}, Context : {message}, ExceptionMessage:{exception.Message}, InnerExceptionMessage:{exception.InnerException?.Message}"); // ## {stringBuilder.ToString()}");
        }

    }
}
