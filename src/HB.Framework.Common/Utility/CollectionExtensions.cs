using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace System.Collections.Generic
{
    public static class CollectionExtensions
    {

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            foreach (T t in enumerable)
            {
                action(t);
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> action)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }


            foreach (T t in enumerable)
            {
                await action(t).ConfigureAwait(false);
            }
        }

        public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> original, IDictionary<TKey, TValue> toAdds)
        {
            ThrowIf.Null(original, nameof(original));
            ThrowIf.Null(toAdds, nameof(toAdds));

            toAdds.ForEach(kv => original.Add(kv.Key, kv.Value));

        }

        public static void Add<T>(this IList<T> original, IEnumerable<T> items)
        {
            ThrowIf.NullOrEmpty(original, nameof(original));
            ThrowIf.NullOrEmpty(items, nameof(items));

            items.ForEach(t => original.Add(t));
        }

        public static IDictionary<TKey, TValue> CloningWithValues<TKey, TValue> (this IDictionary<TKey, TValue> original) where TValue : ICloneable
        {
            if (original == null)
            {
                return null;
            }

            IDictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>();

            foreach (KeyValuePair<TKey, TValue> entry in original)
            {
                try
                {
                    ICloneable cloneable = entry.Value as ICloneable;
                    ret.Add(entry.Key, (TValue)cloneable.Clone());
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return ret;
        }

        public static IDictionary<TKey, int> CloningWithValues<TKey>(this IDictionary<TKey, int> original)
        {
            if (original == null)
            {
                return null;
            }

            IDictionary<TKey, int> ret = new Dictionary<TKey, int>();

            foreach (KeyValuePair<TKey, int> entry in original)
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
            if (original == null)
            {
                return null;
            }

            IDictionary<TKey, TNewValue> ret = new Dictionary<TKey, TNewValue>();

            foreach (var pair in original)
            {
                ret.Add(pair.Key, converter(pair.Value));
            }

            return ret;
        }

        public static IList<T> CloneWithValues<T>(this IList<T> lst) where T : ICloneable
        {
            if (lst == null)
            {
                return null;
            }

            List<T> retList = new List<T>();

            foreach (var item in lst)
            {
                retList.Add((T)item.Clone()); 
            }

            return retList;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> ts)
        {
            return ts == null || ts.Count() == 0;
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> ts)
        {
            return ts != null && ts.Count() != 0;
        }

        public static NameValueCollection ToHttpValueCollection(this IDictionary<string, string> dict)
        {
            ThrowIf.Null(dict, nameof(dict));

            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString("");

            dict.ForEach(kv => nameValueCollection.Add(kv.Key, kv.Value));

            return nameValueCollection;
        }
    }
}