using Microsoft.Extensions.Caching.Distributed;
using RedisApplication.Interfaces;
using System.Text.Json;

namespace RedisApplication.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        public readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T? GetData<T>(string key)
        {
            var cacheData = _cache.GetString(key);

            if (cacheData is null)
            {
                return default(T?);
            }

            return JsonSerializer.Deserialize<T>(cacheData);
        }

        public void SetData<T>(string key, T data)
        {
            var serializedData = JsonSerializer.Serialize(data);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            };

            _cache.SetString(key, serializedData, options);
        }
    }
}
