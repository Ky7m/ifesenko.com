using System;
using System.Threading.Tasks;
using ifesenko.com.Infrastructure.Services.Interfaces;
using ifesenko.com.Infrastructure.Settings;
using Microsoft.Extensions.OptionsModel;
using Microsoft.IO;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ifesenko.com.Infrastructure.Services.Implementation
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

        private readonly RecyclableMemoryStreamManager _memoryStreamManager = new RecyclableMemoryStreamManager();
        private readonly JsonSerializer _serializer = new JsonSerializer { Formatting = Formatting.None };

        public async Task<bool> StoreAsync<T>(string key, T value, TimeSpan? expiry = null) where T: class
        {
            string serializedValue = null;
            if (value != null)
            {
                serializedValue = JsonConvert.SerializeObject(value);
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
