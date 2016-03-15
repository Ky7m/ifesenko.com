using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.IO;
using PersonalHomePage.Services.Interfaces;
using StackExchange.Redis;
using Wire;

namespace PersonalHomePage.Services.Implementation
{
    public sealed class RedisCacheService : ICacheService
    {
        private readonly Lazy<ConnectionMultiplexer> _cacheDatabase = new Lazy<ConnectionMultiplexer>(() =>
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RedisCacheConnectionString"].ConnectionString;
            return ConnectionMultiplexer.Connect(connectionString);
        });

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
