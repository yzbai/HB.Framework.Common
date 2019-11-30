using HB.Framework.Common;
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
            if (dict == null || dict.Count == 0)
            {
                throw new ArgumentException("NullOrEmpty", paramName);
            }

            return dict;
        }

        public static IEnumerable<T> NullOrEmpty<T>([ValidatedNotNull]IEnumerable<T> lst, string paramName)
        {
            if (lst == null || !lst.Any())
            {
                throw new ArgumentException("NullOrEmpty", paramName);
            }

            return lst;
        }

        public static IEnumerable<T> AnyNull<T>([ValidatedNotNull]IEnumerable<T> lst, string paramName)
        {
            if (lst == null || lst.Any(t => t == null))
            {
                throw new ArgumentException("NullOrAnyNull", paramName);
            }

            return lst;
        }



        public static string NullOrEmpty([ValidatedNotNull]string o, string paramName)
        {
            if (string.IsNullOrEmpty(o))
            {
                throw new ArgumentException("NullOrEmpty", paramName);
            }

            return o;
        }

        public static string NotEqual(string a, string b, string paramName)
        {
            if (a == null && b != null || a != null && !a.Equals(b, GlobalSettings.Comparison))
            {
                throw new ArgumentException("ThrowIf_NotEqual_Error_Message", paramName);
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
                throw new ArgumentException("ThrowIfNullOrEmpty", paramName);
            }

            return o;
        }

        public static IDictionary<TKey, TValue> ThrowIfNullOrEmpty<TKey, TValue>([ValidatedNotNull]this IDictionary<TKey, TValue> dict, string paramName)
        {
            if (dict == null || dict.Count == 0)
            {
                throw new ArgumentException("ThrowIfNullOrEmpty", paramName);
            }

            return dict;
        }

        public static IEnumerable<T> ThrowIfNullOrEmpty<T>([ValidatedNotNull]this IEnumerable<T> lst, string paramName)
        {
            if (lst == null || !lst.Any())
            {
                throw new ArgumentException("ThrowIfNullOrEmpty", paramName);
            }

            return lst;
        }


        public static string ThrowIfNotEqual(this string a, string b, string paramName)
        {
            if (a == null && b != null || a != null && !a.Equals(b, GlobalSettings.Comparison))
            {
                throw new ArgumentException("ThrowIf_NotEqual_Error_Message", paramName);
            }

            return a;
        }
    }
}
