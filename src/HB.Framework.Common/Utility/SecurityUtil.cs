﻿#nullable enable

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class SecurityUtil
    {
        //public static string GetSHA1(string str)
        //{
        //    SHA1 sha1 = SHA1.Create();
        //    byte[] buffer = Encoding.UTF8.GetBytes(str);

        //    byte[] sha1Bytes = sha1.ComputeHash(buffer);

        //    return DataConverter.ToHexString(sha1Bytes);

        //}

        //public static string GetMD5(string str)
        //{
        //    MD5 md5 = MD5.Create();
        //    byte[] buffer = Encoding.UTF8.GetBytes(str);
        //    byte[] md5Bytes = md5.ComputeHash(buffer);

        //    return DataConverter.ToHexString(md5Bytes);
        //    //return Convert.ToBase64String(md5Bytes);
        //}

        /// <summary>
        /// GetHash
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="System.Reflection.TargetInvocationException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        public static string GetHash(string item)
        {
            using SHA256 sha256Obj = SHA256.Create();
            byte[] hashBytes = sha256Obj.ComputeHash(Encoding.UTF8.GetBytes(item));

            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// GetHash
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="System.Reflection.TargetInvocationException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        public static async Task<string> GetHashAsync<T>([DisallowNull] T item) where T : class
        {
            using SHA256 sha256Obj = SHA256.Create();
            byte[] result = sha256Obj.ComputeHash(await SerializeUtil.PackAsync(item).ConfigureAwait(false));

            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// EncryptPwdWithSalt
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        /// <exception cref="System.Reflection.TargetInvocationException">Ignore.</exception>
        /// <exception cref="System.ObjectDisposedException">Ignore.</exception>
        public static string EncryptPwdWithSalt(string pwd, string salt)
        {
            return GetHash(pwd + salt);
        }
        public static string CreateUniqueToken()
        {
            return Guid.NewGuid().ToString("N", GlobalSettings.Culture);
        }

        #region Random String

        private const string _charCollection = "0,1,2,3,4,5,6,7,8,9,a,s,d,f,g,h,z,c,v,b,n,m,k,q,w,e,r,t,y,u,p,A,S,D,F,G,H,Z,C,V,B,N,M,K,Q,W,E,R,T,Y,U,P"; //定义验证码字符及出现频次 ,避免出现0 o j i l 1 x;
        private static readonly string[] _charArray = _charCollection.Split(',');
        private static readonly string[] _numbericCharArray = _charCollection.Substring(0, 20).Split(',');

        public static string CreateRandomString(int length)
        {
            return CreateRandomString(length, _charArray);
        }

        public static string CreateRandomNumbericString(int length)
        {
            return CreateRandomString(length, _numbericCharArray);
        }

        private static string CreateRandomString(int length, string[] charArray)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int arrayLength = charArray.Length - 1;
            string randomString = "";

            for (int i = 0; i < length; i++)
            {
                randomString += charArray[random.Next(0, arrayLength)];
            }

            return randomString;
        }

        #endregion

        public static long GetCurrentTimestamp()
        {
            TimeSpan ts = DateTimeOffset.UtcNow - new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        public static byte[] HexToByteArray(string hexString)
        {
            byte[] bytes = new byte[hexString.Length / 2];

            for (int i = 0; i < hexString.Length; i += 2)
            {
                string s = hexString.Substring(i, 2);
                bytes[i / 2] = byte.Parse(s, NumberStyles.HexNumber, null);
            }

            return bytes;
        }
    }
}
