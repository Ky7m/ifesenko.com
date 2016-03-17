using System;
using System.Threading.Tasks;
using ifesenko.com.Infrastructure.Services.Interfaces;
using ifesenko.com.Infrastructure.Settings;
using Microsoft.Extensions.OptionsModel;
using Microsoft.IO;
using StackExchange.Redis;
using Wire;

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
        private readonly Serializer _serializer = new Serializer(new SerializerOptions(true));

        public async Task<bool> StoreAsync<T>(string key, T value, TimeSpan? expiry = null) where T : class
        {
            var serializedValue = Serialize(value);
            return await _cacheDatabase.Value.GetDatabase().StringSetAsync(key, serializedValue, expiry, flags: CommandFlags.FireAndForget);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var database = _cacheDatabase.Value.GetDatabase();
            var serializedValue = await database.StringGetAsync(key);
            return !string.IsNullOrEmpty(serializedValue) ? await DeserializeAsync<T>(serializedValue) : default(T);
        }
        public async Task<bool> DeleteAsync(string key)
        {
            return await _cacheDatabase.Value.GetDatabase().KeyDeleteAsync(key, CommandFlags.FireAndForget);
        }

        private byte[] Serialize<T>(T value) where T : class
        {
            byte[] objectDataAsStream;
            if (value == null)
            {
                return null;
            }
            using (var memoryStream = _memoryStreamManager.GetStream())
            {
                _serializer.Serialize(value, memoryStream);
                objectDataAsStream = memoryStream.ToArray();
            }
            return objectDataAsStream;
        }

        private async Task<T> DeserializeAsync<T>(byte[] sourceBytes)
        {
            var result = default(T);
            if (sourceBytes == null)
            {
                return result;
            }
            using (var memoryStream = _memoryStreamManager.GetStream())
            {
                await memoryStream.WriteAsync(sourceBytes, 0, sourceBytes.Length);
                memoryStream.Position = 0;
                result = _serializer.Deserialize<T>(memoryStream);
            }

            return result;
        }
    }
}
