#nullable enable

using HB.Framework.Common.Cache;
using System;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Caching.Distributed
{
    public static class IDistributedCacheExtensions
    {
        #region Generic

        /// <summary>
        /// SetAsync
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="HB.Framework.Common.Cache.CacheException"></exception>
        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options) where T : class
        {
            try
            {
                byte[] bytes = await SerializeUtil.PackAsync(value).ConfigureAwait(false);

                await cache.SetAsync(key, bytes, options).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new CacheException($"Cache SetAsync Error. Key:{key}, Value:{SerializeUtil.ToJson(value)}", ex);
            }
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="HB.Framework.Common.Cache.CacheException"></exception>
        public static async Task<T?> GetAsync<T>(this IDistributedCache cache, string key) where T : class
        {
            try
            {
                byte[] bytes = await cache.GetAsync(key).ConfigureAwait(false);
                return await SerializeUtil.UnPackAsync<T>(bytes).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new CacheException($"Cache GetAsync Error. Key:{key}", ex);
            }
        }

        /// <summary>
        /// SetIntAsync
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="CacheException"></exception>
        public static async Task SetIntAsync(this IDistributedCache cache, string key, int value, DistributedCacheEntryOptions options)
        {
            try
            {
                await cache.SetStringAsync(key, Convert.ToString(value, GlobalSettings.Culture), options).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new CacheException($"Cache SetIntAsync Error. Key:{key}, Value:{value}", ex);
            }
        }

        /// <summary>
        /// GetIntAsync
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="CacheException"></exception>
        public static async Task<int> GetIntAsync(this IDistributedCache cache, string key)
        {
            try
            {
                string value = await cache.GetStringAsync(key).ConfigureAwait(false);

                return value.ToInt32();
            }
            catch (Exception ex)
            {
                throw new CacheException($"Cache GetIntAsync Error. Key:{key}", ex);
            }
        }

        /// <summary>
        /// 如果存在就移除
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns>true:存在; false: 不存在</returns>
        /// <exception cref="CacheException"></exception>
        public static async Task<bool> IsExistThenRemoveAsync(this IDistributedCache cache, string key)
        {
            try
            {
                byte[] result = await cache.GetAsync(key).ConfigureAwait(false);

                if (result != null && result.Length > 0)
                {
                    await cache.RemoveAsync(key).ConfigureAwait(false);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new CacheException($"Cache IsExistThenRemoveAsync Error. Key:{key}", ex);
            }
        }

        #endregion

    }
}
