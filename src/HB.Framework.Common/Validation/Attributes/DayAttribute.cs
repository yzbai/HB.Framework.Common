#nullable enable

using HB.Framework.Common.Validate;
using System.ComponentModel.DataAnnotations;

namespace HB.Framework.Common.Validation.Attributes
{
    public sealed class DayAttribute : ValidationAttribute
    {
        public bool CanBeNull { get; set; } = true;

        public DayAttribute()
        {
            if (string.IsNullOrEmpty(ErrorMessage))
            {
                ErrorMessage = "Not a valid Day.";
            }
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return CanBeNull;
            }

            return value is string text && ValidationMethods.IsDay(text);
        }
    }
}
