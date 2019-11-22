using System;
using System.Runtime.Serialization;

namespace HB.Framework.Common
{
    public class ValidateErrorException : Exception
    {
        public ValidateErrorException()
        {
        }

        public ValidateErrorException(ISupportValidate supportValidate) : base(supportValidate?.GetValidateErrorMessage())
        {
        }

        public ValidateErrorException(string message) : base(message)
        {
        }

        public ValidateErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValidateErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
