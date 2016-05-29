using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PersonalWebApp.Infrastructure.Services.Interfaces;
using PersonalWebApp.Infrastructure.Settings;
using StackExchange.Redis;

namespace PersonalWebApp.Infrastructure.Services.Implementation
{
    public sealed class RedisCacheService : ICacheService
    {
        private readonly AppSettings _appSettings;
        private readonly Lazy<ConnectionMultiplexer> _cacheDatabase;
        public RedisCacheService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _cacheDatabase = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_appSettings.RedisCacheConnectionString));
        }

        public async Task<bool> StoreAsync<T>(string key, T value, TimeSpan? expiry = null) where T: class
        {
            string serializedValue = null;
            if (value != null)
            {
                serializedValue = JsonConvert.SerializeObject(value, Formatting.None);
            }
            return await _cacheDatabase.Value.GetDatabase().StringSetAsync(key, serializedValue, expiry);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var database = _cacheDatabase.Value.GetDatabase();
            var serializedValue = await database.StringGetAsync(key);
            if (string.IsNullOrEmpty(serializedValue))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(serializedValue);
        }
        public async Task<bool> DeleteAsync(string key)
        {
            return await _cacheDatabase.Value.GetDatabase().KeyDeleteAsync(key);
        }
    }
}
