using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PersonalWebApp.Infrastructure.Services.Interfaces;
using PersonalWebApp.Infrastructure.Settings;
using StackExchange.Redis;
using Wire;

namespace PersonalWebApp.Infrastructure.Services.Implementation
{
    public sealed class RedisCacheService : ICacheService
    {
        private readonly AppSettings _appSettings;
        private readonly Lazy<ConnectionMultiplexer> _cacheDatabase;
        private readonly Serializer _serializer = new Serializer(new SerializerOptions(true));

        public RedisCacheService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _cacheDatabase = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_appSettings.RedisCacheConnectionString));
        }

        public async Task<bool> StoreAsync<T>(string key, T value, TimeSpan? expiry = null) where T: class
        {
            var serializedValue = Serialize(value);
            return await _cacheDatabase.Value.GetDatabase().StringSetAsync(key, serializedValue, expiry);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var database = _cacheDatabase.Value.GetDatabase();
            var serializedValue = await database.StringGetAsync(key);
            return string.IsNullOrEmpty(serializedValue) ? default(T) : Deserialize<T>(serializedValue);
        }
        public async Task<bool> DeleteAsync(string key)
        {
            return await _cacheDatabase.Value.GetDatabase().KeyDeleteAsync(key);
        }

        #region Private methods

        private byte[] Serialize<T>(T value) where T:class
        {
            byte[] objectDataAsStream;
            if (value == null)
            {
                return null;
            }
            using (var memoryStream = new MemoryStream())
            {
                _serializer.Serialize(value, memoryStream);
                objectDataAsStream = memoryStream.ToArray();
            }
            return objectDataAsStream;
        }

        private T Deserialize<T>(byte[] sourceBytes)
        {
            var result = default(T);
            if (sourceBytes == null)
            {
                return result;
            }
            using (var memoryStream = new MemoryStream())
            {
                memoryStream.Write(sourceBytes, 0, sourceBytes.Length);
                memoryStream.Position = 0;
                result = _serializer.Deserialize<T>(memoryStream);
            }

            return result;
        }

        #endregion
    }
}
