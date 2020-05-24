#nullable enable

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace System
{
    public static class CollectionExtensions
    {

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T t in enumerable.ThrowIfNull(nameof(enumerable)))
            {
                action(t);
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> action)
        {
            foreach (T t in enumerable.ThrowIfNull(nameof(enumerable)))
            {
                await action(t).ConfigureAwait(false);
            }

        }

        public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> original, IDictionary<TKey, TValue> toAdds)
        {
            toAdds.ForEach(kv => original.Add(kv.Key, kv.Value));
        }

        public static void Add<T>(this IList<T> original, IEnumerable<T> items)
        {
            items.ForEach(t => original.Add(t));
        }

        public static IDictionary<TKey, TValue> CloningWithValues<TKey, TValue>(this IDictionary<TKey, TValue> original) where TValue : ICloneable
        {
            IDictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>();

            foreach (KeyValuePair<TKey, TValue> entry in original.ThrowIfNull(nameof(original)))
            {
                ICloneable cloneable = entry.Value as ICloneable;
                ret.Add(entry.Key, (TValue)cloneable.Clone());

            }
            return ret;
        }

        public static IDictionary<TKey, int> CloningWithValues<TKey>(this IDictionary<TKey, int> original)
        {
            IDictionary<TKey, int> ret = new Dictionary<TKey, int>();

            foreach (KeyValuePair<TKey, int> entry in original.ThrowIfNull(nameof(original)))
            {
                try
                {
                    ret.Add(entry.Key, entry.Value);
                }
                catch
                {
                    throw;
                }
            }
            return ret;
        }

        public static IDictionary<TKey, TNewValue> ConvertValue<TKey, TValue, TNewValue>(this IDictionary<TKey, TValue> original, Func<TValue, TNewValue> converter)
        {
            IDictionary<TKey, TNewValue> ret = new Dictionary<TKey, TNewValue>();

            foreach (KeyValuePair<TKey, TValue> pair in original.ThrowIfNull(nameof(original)))
            {
                ret.Add(pair.Key, converter(pair.Value));
            }

            return ret;
        }

        public static IList<T> CloneWithValues<T>(this IList<T> lst) where T : ICloneable
        {
            List<T> retList = new List<T>();

            foreach (T item in lst.ThrowIfNull(nameof(lst)))
            {
                retList.Add((T)item.Clone());
            }

            return retList;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? ts)
        {
            return ts == null || !ts.Any();
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T>? ts)
        {
            return ts != null && ts.Any();
        }

        public static bool IsNotNullOrEmpty(this Array? array)
        {
            return array != null && array.Length != 0;
        }

        public static NameValueCollection ToHttpValueCollection(this IEnumerable<KeyValuePair<string, string>> dict)
        {
            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString("");

            dict.ForEach(kv => nameValueCollection.Add(kv.Key, kv.Value));

            return nameValueCollection;
        }

        public static string? ToJoinedString(this IEnumerable ts, string seprator)
        {
            if (ts == null)
            {
                return null;
            }

            StringBuilder stringBuilder = new StringBuilder();

            foreach (object obj in ts)
            {
                stringBuilder.Append(obj.ToString());
                stringBuilder.Append(seprator);
            }

            if (stringBuilder.Length != 0)
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }

            return stringBuilder.ToString();
        }
    }
}