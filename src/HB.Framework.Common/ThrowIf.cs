using HB.Framework.Common;
using HB.Framework.Common.Validate;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class ValidatedNotNullAttribute : Attribute
    {
    }

    public static class ThrowIf
    {
        public static T Null<T>([ValidatedNotNull]T o, string paramName) where T : class
        {
            if (o == null)
                throw new ArgumentNullException(paramName);

            return o;
        }

        public static T NullOrNotValid<T>([ValidatedNotNull]T o, string paramName) where T : ValidatableObject
        {
            if (o == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (!o.IsValid())
            {
                throw new ValidateErrorException(o);
            }

            return o;
        }

        public static IEnumerable<T> NullOrNotValid<T>([ValidatedNotNull]IEnumerable<T> ts, string paramName) where T : ValidatableObject
        {
            if (ts == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (ts.Any())
            {
                ts.ForEach(t =>
                {
                    if (t != null)
                    {
                        if (!t.IsValid())
                        {
                            throw new ValidateErrorException(t);
                        }
                    }
                });
            }

            return ts;
        }

        public static IDictionary<TKey, TValue> NullOrEmpty<TKey, TValue>([ValidatedNotNull]IDictionary<TKey, TValue> dict, string paramName)
        {
            if (dict == null || !dict.Any())
            {
                throw new ArgumentException(HB.Framework.Common.Properties.Resources.DictionaryNullOrEmptyErrorMessage, paramName);
            }

            return dict;
        }

        public static IEnumerable<T> NullOrEmpty<T>([ValidatedNotNull]IEnumerable<T> lst, string paramName)
        {
            if (lst == null || !lst.Any())
            {
                throw new ArgumentException(HB.Framework.Common.Properties.Resources.CollectionNullOrEmptyErrorMessage, paramName);
            }

            return lst;
        }

        public static IEnumerable<T> AnyNull<T>([ValidatedNotNull]IEnumerable<T> lst, string paramName)
        {
            if (lst == null || lst.Any(t => t == null))
            {
                throw new ArgumentException(HB.Framework.Common.Properties.Resources.CollectionAnyNullErrorMessage, paramName);
            }

            return lst;
        }



        public static string NullOrEmpty([ValidatedNotNull]string o, string paramName)
        {
            if (string.IsNullOrEmpty(o))
            {
                throw new ArgumentException(HB.Framework.Common.Properties.Resources.ParameterNullOrEmptyErrorMessage, paramName);
            }

            return o;
        }

        public static string NullOrNotMobile([ValidatedNotNull]string mobile, string paramName)
        {
            if (string.IsNullOrEmpty(mobile) || !ValidationMethods.IsMobilePhone(mobile))
            {
                throw new ArgumentException(HB.Framework.Common.Properties.Resources.NotMobileErrorMessage, paramName);
            }

            return mobile;
        }

        public static string NotPassword([ValidatedNotNull]string password, string paramName, bool canBeNull)
        {
            if (canBeNull && password == null)
            {
                return password;
            }

            if (string.IsNullOrEmpty(password) || !ValidationMethods.IsPassword(password))
            {
                throw new ArgumentException(HB.Framework.Common.Properties.Resources.NotPasswordErrorMessage, paramName);
            }

            return password;
        }

        public static string NullOrNotUserName([ValidatedNotNull]string userName, string paramName)
        {
            if (string.IsNullOrEmpty(userName) || !ValidationMethods.IsUserName(userName))
            {
                throw new ArgumentException(HB.Framework.Common.Properties.Resources.NotUserNameErrorMessage, paramName);
            }

            return userName;
        }

        public static string NullOrNotEmail([ValidatedNotNull]string email, string paramName)
        {
            if (string.IsNullOrEmpty(email) || !ValidationMethods.IsEmail(email))
            {
                throw new ArgumentException(HB.Framework.Common.Properties.Resources.NotEmailErrorMessage, paramName);
            }

            return email;
        }

        public static string NotEqual(string a, string b, string paramName)
        {
            if (a == null && b != null || a != null && !a.Equals(b, GlobalSettings.Comparison))
            {
                throw new ArgumentException(HB.Framework.Common.Properties.Resources.StringNotEqualErrorMessage, paramName);
            }

            return a;
        }
    }
    public static class ThrowIfExtensions
    {


        public static T ThrowIfNull<T>([ValidatedNotNull]this T o, string paramName) where T : class
        {
            if (o == null)
                throw new ArgumentNullException(paramName);

            return o;
        }

        public static string ThrowIfNullOrEmpty([ValidatedNotNull]this string o, string paramName)
        {
            if (string.IsNullOrEmpty(o))
            {
                throw new ArgumentException(HB.Framework.Common.Properties.Resources.ParameterNullOrEmptyErrorMessage, paramName);
            }

            return o;
        }

        public static IDictionary<TKey, TValue> ThrowIfNullOrEmpty<TKey, TValue>([ValidatedNotNull]this IDictionary<TKey, TValue> dict, string paramName)
        {
            if (dict == null || !dict.Any())
            {
                throw new ArgumentException(HB.Framework.Common.Properties.Resources.DictionaryNullOrEmptyErrorMessage, paramName);
            }

            return dict;
        }

        public static IEnumerable<T> ThrowIfNullOrEmpty<T>([ValidatedNotNull]this IEnumerable<T> lst, string paramName)
        {
            if (lst == null || !lst.Any())
            {
                throw new ArgumentException(HB.Framework.Common.Properties.Resources.CollectionNullOrEmptyErrorMessage, paramName);
            }

            return lst;
        }


        public static string ThrowIfNotEqual(this string a, string b, string paramName)
        {
            if (a == null && b != null || a != null && !a.Equals(b, GlobalSettings.Comparison))
            {
                throw new ArgumentException(HB.Framework.Common.Properties.Resources.StringNotEqualErrorMessage, paramName);
            }

            return a;
        }
    }
}
