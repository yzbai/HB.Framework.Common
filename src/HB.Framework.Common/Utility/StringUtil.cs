﻿#nullable enable

using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class StringUtil
    {
        #region String Encode to bytes

        public static byte[] GetUTF8Bytes(string item)
        {
            return Encoding.UTF8.GetBytes(item);
        }

        public static string GetUTF8String(byte[] item)
        {
            return Encoding.UTF8.GetString(item);
        }

        public static string ToHexString(byte[] bytes)
        {
            ThrowIf.Null(bytes, nameof(bytes));

            StringBuilder hex = new StringBuilder();

            foreach (byte b in bytes)
            {
                hex.AppendFormat(GlobalSettings.Culture, "{0:x2}", b);
            }

            return hex.ToString();
        }

        #endregion

        #region Collection to String

        private static readonly string[] _separator = { "-)#@$(-" };

        private static readonly int _separatorLength = _separator[0].Length;

        public static string ListToString(IEnumerable<string> list)
        {
            StringBuilder builder = new StringBuilder();
            bool added = false;

            foreach (string str in ThrowIf.Null(list, nameof(list)))
            {
                builder.Append(str);
                builder.Append(_separator[0]);

                added = true;
            }

            if (added)
            {
                builder.Remove(builder.Length - _separatorLength, _separatorLength);
            }

            return builder.ToString();
        }

        public static IList<string> StringToList(string? longStr)
        {
            List<string> list = new List<string>();

            if (string.IsNullOrEmpty(longStr))
            {
                return list;
            }

            string[] results = longStr.Split(_separator, StringSplitOptions.None);

            foreach (string str in results)
            {
                list.Add(str);
            }

            return list;
        }

        public static string? DictionaryToString(IDictionary<string, string>? subjectNodeSetIds)
        {
            if (subjectNodeSetIds == null || subjectNodeSetIds.Count == 0)
            {
                return null;
            }

            StringBuilder builder = new StringBuilder();
            bool added = false;

            foreach (KeyValuePair<string, string> kv in subjectNodeSetIds)
            {
                builder.Append(kv.Key);
                builder.Append(_separator[0]);
                builder.Append(kv.Value);
                builder.Append(_separator[0]);

                added = true;
            }

            if (added)
            {
                builder.Remove(builder.Length - _separatorLength, _separatorLength);
            }

            return builder.ToString();
        }

        public static IDictionary<string, string> StringToDictionary(string? jointSubjectNodeSetIds)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(jointSubjectNodeSetIds))
            {
                return dict;
            }

            string[] result = jointSubjectNodeSetIds.Split(_separator, StringSplitOptions.None);

            if (result.Length % 2 != 0)
            {
                return dict;
            }

            for (int i = 0; i < result.Length; i += 2)
            {
                dict.Add(result[i], result[i + 1]);
            }

            return dict;
        }

        #endregion

        #region Extensions

        public static bool IsIn(this string? str, params string?[] words)
        {
            foreach (string? word in words)
            {
                bool isEqual = str == null ? word == null : str.Equals(word, GlobalSettings.Comparison);

                if (isEqual)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断是否是全大写字母组成
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsAllUpper(this string str)
        {
            foreach (char c in str.ThrowIfNullOrEmpty(nameof(str)))
            {
                if (!char.IsUpper(c))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 判断是否是全小写字母组成
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsAllLower(this string str)
        {
            foreach (char c in str.ThrowIfNullOrEmpty(nameof(str)))
            {
                if (!char.IsLower(c))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsNullOrEmpty([ValidatedNotNull]this string? str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNotNullOrEmpty([ValidatedNotNull]this string? str)
        {
            return !string.IsNullOrEmpty(str);
        }

        //太容易产生Bug
        //public static bool IsNotNullOrEmpty(this string str)
        //{
        //    return !string.IsNullOrEmpty(str);
        //}

        public static string RemoveSuffix(this string str, string suffix)
        {
            return str.ThrowIfNull(nameof(str)).EndsWith(suffix, GlobalSettings.Comparison) 
                ? str.Substring(0, str.Length - suffix.ThrowIfNull(nameof(suffix)).Length) 
                : str;
        }

        #endregion
    }
}
