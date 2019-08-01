﻿using HB.Framework.Common;
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

        public static IDictionary<TKey, TValue> NullOrEmpty<TKey, TValue>([ValidatedNotNull]IDictionary<TKey, TValue> dict, string paramName)
        {
            if (dict == null || dict.Count == 0)
            {
                throw new ArgumentException(paramName);
            }

            return dict;
        }

        public static IEnumerable<T> NullOrEmpty<T>([ValidatedNotNull]IEnumerable<T> lst, string paramName)
        {
            if (lst == null || !lst.Any())
            {
                throw new ArgumentException(paramName);
            }

            return lst;
        }

        public static string NullOrEmpty([ValidatedNotNull]string o, string paramName)
        {
            if (string.IsNullOrEmpty(o))
            {
                throw new ArgumentException(paramName);
            }

            return o;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
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
            if(string.IsNullOrEmpty(o))
            {
                throw new ArgumentException(paramName);
            }

            return o;
        }

        public static IDictionary<TKey, TValue> ThrowIfNullOrEmpty<TKey, TValue>([ValidatedNotNull]this IDictionary<TKey, TValue> dict, string paramName)
        {
            if (dict == null || dict.Count == 0)
            {
                throw new ArgumentException(paramName);
            }

            return dict;
        }

        public static IEnumerable<T> ThrowIfNullOrEmpty<T>([ValidatedNotNull]this IEnumerable<T> lst, string paramName)
        {
            if (lst == null || !lst.Any())
            {
                throw new ArgumentException(paramName);
            }

            return lst;
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
        public static string ThrowIfNotEqual(this string a, string b, string paramName)
        {
            if (a == null && b!=null || a!=null && !a.Equals(b, GlobalSettings.Comparison))
            {
                throw new ArgumentException("ThrowIf_NotEqual_Error_Message", paramName);
            }

            return a;
        }
    }
}
