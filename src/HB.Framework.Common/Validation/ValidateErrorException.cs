#nullable enable

using System;
using System.Runtime.Serialization;

namespace HB.Framework.Common
{
    public class ValidateErrorException : FrameworkException
    {
        public override FrameworkExceptionType ExceptionType { get => FrameworkExceptionType.Validation; }

        public ValidateErrorException()
        {
        }

        public ValidateErrorException(ISupportValidate supportValidate) : this(supportValidate?.GetValidateErrorMessage())
        {
        }

        public ValidateErrorException(string? message) : base(message)
        {
        }

        public ValidateErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}

#nullable restore