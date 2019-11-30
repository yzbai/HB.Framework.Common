using System.Threading.Tasks;
using HB.Framework.Common;
using System.Text;
using System;

namespace Microsoft.Extensions.Caching.Distributed
{
    public static class IDistributedCacheExtensions
    {
        #region Generic

        public static void Set<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options) where T :class
        {
            ThrowIf.Null(cache, nameof(cache));
            ThrowIf.NullOrEmpty(key, nameof(key));

            cache.Set(key, SerializeUtil.Pack(value), options);
        }

        public static Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options) where T : class
        {
            ThrowIf.Null(cache, nameof(cache));
            ThrowIf.NullOrEmpty(key, nameof(key));

            return cache.SetAsync(key, SerializeUtil.Pack(value), options);
        }

        public static T Get<T>(this IDistributedCache cache, string key) where T : class
        {
            ThrowIf.Null(cache, nameof(cache));
            ThrowIf.NullOrEmpty(key, nameof(key));

            byte[] bytes = cache.Get(key);
            return SerializeUtil.UnPack<T>(bytes);
        }

        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key) where T : class
        {
            ThrowIf.Null(cache, nameof(cache));
            ThrowIf.NullOrEmpty(key, nameof(key));

            byte[] bytes = await cache.GetAsync(key).ConfigureAwait(false);
            return SerializeUtil.UnPack<T>(bytes);
        }

        public static void SetInt(this IDistributedCache cache, string key, int value, DistributedCacheEntryOptions options)
        {
            ThrowIf.Null(cache, nameof(cache));
            ThrowIf.NullOrEmpty(key, nameof(key));

            cache.SetString(key, Convert.ToString(value, GlobalSettings.Culture), options);
        }

        public static int? GetInt(this IDistributedCache cache, string key)
        {
            ThrowIf.Null(cache, nameof(cache));
            ThrowIf.NullOrEmpty(key, nameof(key));

            string value = cache.GetString(key);

            return Convert.ToInt32(value, GlobalSettings.Culture);
        }

        /// <summary>
        /// 如果存在就移除
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns>true:存在; false: 不存在</returns>
        public static async Task<bool> RemoveIfExistAsync(this IDistributedCache cache, string key)
        {
            ThrowIf.Null(cache, nameof(cache));
            ThrowIf.NullOrEmpty(key, nameof(key));

            byte[] result = await cache.GetAsync(key).ConfigureAwait(false);

            if (result != null && result.Length > 0)
            {
                await cache.RemoveAsync(key).ConfigureAwait(false);
                return true;
            }

            return false;
        }

        #endregion
        
    }
}
