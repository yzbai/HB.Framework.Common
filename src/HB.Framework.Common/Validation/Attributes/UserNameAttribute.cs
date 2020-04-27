#nullable enable

using HB.Framework.Common.Validate;

namespace System.ComponentModel.DataAnnotations
{
    public sealed class UserNameAttribute : ValidationAttribute
    {
        public bool CanBeNull { get; set; } = true;

        public UserNameAttribute()
        {
            if (string.IsNullOrEmpty(ErrorMessage))
            {
                ErrorMessage = "Not a valid UserName.";
            }
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return CanBeNull;
            }

            return value is string text
                && text.Length <= ValidationSettings.UserNameMaxLength
                && ValidationMethods.IsUserName(text);
        }
    }
}
