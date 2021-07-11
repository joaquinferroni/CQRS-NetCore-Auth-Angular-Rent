using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Rental.Application.Infrastructure.Cache
{
    public static class CacheExtensions
    {
        public static async Task<T> GetCacheValueAsync<T>(this IDistributedCache cache, CacheKeysEnum key) where T : class
        {
            string result = await cache.GetStringAsync(key.ToString());
            if (String.IsNullOrEmpty(result))
                return null;
            return JsonSerializer.Deserialize<T>(result);
        }

        public static async Task SetCacheValueAsync<T>(this IDistributedCache cache, CacheKeysEnum key, T value) where T : class
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
                SlidingExpiration = TimeSpan.FromSeconds(30)
            };

            string result = JsonSerializer.Serialize(value);

            await cache.SetStringAsync(key.ToString(), result);
        }
    }
}
