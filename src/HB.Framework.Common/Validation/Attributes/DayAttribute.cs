using HB.Framework.Common.Validate;
using System.ComponentModel.DataAnnotations;

namespace HB.Framework.Common.Validation.Attributes
{
    public sealed class DayAttribute : ValidationAttribute
    {
        public DayAttribute()
        {
            if (string.IsNullOrEmpty(ErrorMessage))
            {
                //TODO: 修改项目中所有ValidationAttribute的ErrorMessage
                ErrorMessage = "xxxxx";
            }
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }

            return value is string text && ValidationMethods.IsDay(text);
        }
    }
}
