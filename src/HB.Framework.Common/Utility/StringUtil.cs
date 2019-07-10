﻿using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class StringUtil
    {
        #region String Encode to bytes

        public static byte[] GetUTF8Bytes(string item)
        {
            if (item == null)
            {
                return null;
            }
            return Encoding.UTF8.GetBytes(item);
        }

        public static string GetUTF8String(byte[] item)
        {
            if (item == null)
            {
                return null;
            }

            return Encoding.UTF8.GetString(item);
        }

        public static string ToHexString(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder();

            foreach (byte b in bytes)
            {
                hex.AppendFormat(GlobalSettings.Culture, "{0:x2}", b);
            }

            return hex.ToString();
        }

        #endregion

        #region Collection to String

        private static readonly string[] separator = { "-)#@$(-" };
        private static readonly int separatorLength = separator[0].Length;
        public static string ListToString(IEnumerable<string> list)
        {
            StringBuilder builder = new StringBuilder();
            bool added = false;

            foreach (string str in list)
            {
                builder.Append(str);
                builder.Append(separator[0]);

                added = true;
            }

            if (added)
            {
                builder.Remove(builder.Length - separatorLength, separatorLength);
            }

            return builder.ToString();
        }

        public static IList<string> StringToList(string longStr)
        {
            List<string> list = new List<string>();

            if (string.IsNullOrEmpty(longStr))
            {
                return list;
            }

            string[] results = longStr.Split(separator, StringSplitOptions.None);

            foreach (string str in results)
            {
                list.Add(str);
            }

            return list;
        }

        public static string DictionaryToString(IDictionary<string, string> subjectNodeSetIds)
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
                builder.Append(separator[0]);
                builder.Append(kv.Value);
                builder.Append(separator[0]);

                added = true;
            }

            if (added)
            {
                builder.Remove(builder.Length - separatorLength, separatorLength);
            }

            return builder.ToString();
        }

        public static IDictionary<string, string> StringToDictionary(string jointSubjectNodeSetIds)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(jointSubjectNodeSetIds))
            {
                return dict;
            }

            string[] result = jointSubjectNodeSetIds.Split(separator, StringSplitOptions.None);

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

        public static bool IsIn(this string str, params string[] words)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (words == null || words.Length == 0)
            {
                return false;
            }

            foreach (string word in words)
            {
                if (str.Equals(word, GlobalSettings.Comparison))
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
            if (str == null || str.Length == 0)
            {
                return false;
            }

            foreach (char c in str)
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
            if (str == null || str.Length == 0)
            {
                return false;
            }

            foreach (char c in str)
            {
                if (!char.IsLower(c))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static void RequireNotNullOrEmpty(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException();
            }
        }

        #endregion
    }
}
