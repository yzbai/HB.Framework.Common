using HB.Framework.Common;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class ThrowIf
    {
        public static T Null<T>(this T o, string paramName) where T : class
        {
            if (o == null)
                throw new ArgumentNullException(paramName);

            return o;
        }

        public static string NullOrEmpty(this string o, string paramName)
        {
            if(string.IsNullOrEmpty(o))
            {
                throw new ArgumentNullException(paramName);
            }

            return o;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "<Pending>")]
        public static string NotEqual(this string a, string b, string paramName)
        {
            if (a == null && b!=null || a!=null && !a.Equals(b, GlobalSettings.Comparison))
            {
                throw new ArgumentException("ThrowIf_NotEqual_Error_Message", paramName);
            }

            return a;
        }
    }
}
