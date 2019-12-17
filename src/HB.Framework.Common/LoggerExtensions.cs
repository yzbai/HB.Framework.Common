using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
    public static class LoggerExtensions
    {
        public static void LogException(this ILogger logger, Exception exception, string message =null, LogLevel logLevel = LogLevel.Error, [CallerMemberName]string caller = "")
        {
            ThrowIf.Null(logger, nameof(logger));
            ThrowIf.Null(exception, nameof(exception));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (string key in exception.Data.Keys)
            {
                stringBuilder.Append($"{key}:{exception.Data[key].ToString()}, ");
            }

            if (exception.InnerException != null)
            {
                foreach (string key in exception.InnerException.Data)
                {
                    stringBuilder.Append($"{key}:{exception.InnerException.Data[key].ToString()}, ");
                }
            }

            logger.Log(logLevel, exception, $"Caller : {caller}, Context : {message}, ExceptionMessage:{exception.Message}, InnerExceptionMessage:{exception.InnerException?.Message} ## {stringBuilder.ToString()}");
        }

    }
}
