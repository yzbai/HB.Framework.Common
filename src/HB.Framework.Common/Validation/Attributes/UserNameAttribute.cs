﻿namespace System.ComponentModel.DataAnnotations
{
    public sealed class UserNameAttribute : ValidationAttribute
    {
        public UserNameAttribute()
        {
            if (string.IsNullOrEmpty(ErrorMessage))
            {
                ErrorMessage = "xxxx";
            }
        }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }

            string? str = value as string;

            if (string.IsNullOrEmpty(str) || str.Length > 50)
            {
                return false;
            }

            return HB.Framework.Common.Validate.ValidationMethods.IsUserName(str);
        }
    }
}
