using System;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using PersonalHomePage.Services.Interfaces;
using StackExchange.Redis;

namespace PersonalHomePage.Services.Implementation
{
    public sealed class RedisCacheService : ICacheService
    {
        private readonly Lazy<ConnectionMultiplexer> _cacheDatabase = new Lazy<ConnectionMultiplexer>(() =>
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RedisCacheConnectionString"].ConnectionString;
            return ConnectionMultiplexer.Connect(connectionString);
        });

        public async Task<bool> StoreAsync<T>(string key, T value, TimeSpan? expiry = null) where T : class
        {
            var serializedValue = Serialize(value);
            return await _cacheDatabase.Value.GetDatabase().StringSetAsync(key, serializedValue, expiry, flags: CommandFlags.FireAndForget);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var database = _cacheDatabase.Value.GetDatabase();
            var serializedValue = await database.StringGetAsync(key);
            return !string.IsNullOrEmpty(serializedValue) ? Deserialize<T>(serializedValue) : default(T);
        }
        public async Task<bool> DeleteAsync(string key)
        {
            return await _cacheDatabase.Value.GetDatabase().KeyDeleteAsync(key, CommandFlags.FireAndForget);
        }

        private static byte[] Serialize<T>(T value) where T : class
        {
            byte[] objectDataAsStream;
            if (value == null)
            {
                return null;
            }
            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, value);
                objectDataAsStream = memoryStream.ToArray();
            }
            return objectDataAsStream;
        }

        private static T Deserialize<T>(byte[] stream)
        {
            var result = default(T);
            if (stream == null)
            {
                return result;
            }
            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream(stream))
            {
                result = (T)binaryFormatter.Deserialize(memoryStream);
            }

            return result;
        }
    }
}
