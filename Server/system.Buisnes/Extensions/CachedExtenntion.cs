using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CRMSystem.Buisnes.Extensions;

public static class CachedExtenntion
{
    public static async Task<T> GetOrCreateAsync<T>(
        this IDistributedCache cache,
        string key,
        Func<Task<T>> factory,
        TimeSpan? expiration = null,
        ILogger? logger = null)
    {
        var cachedData = await cache.GetStringAsync(key);

        if (!string.IsNullOrEmpty(cachedData))
        {
            logger?.LogInformation("Returning {Type} from cache", typeof(T).Name);
            var result = JsonConvert.DeserializeObject<T>(cachedData);

            if (result != null) 
                return result;
        }

        logger?.LogInformation("Returning {Type} from Db", typeof(T).Name);
        var data = await factory();

        if (data != null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromHours(24)
            };

            await cache.SetStringAsync(key, JsonConvert.SerializeObject(data), options);
            logger?.LogInformation("Caching {Type} success", typeof(T).Name);
        }

        return data;
    }
}
