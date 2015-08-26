using System;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace PersonalHomePage.Services
{
    public sealed class RedisCacheService
    {
        private readonly Lazy<IDatabase> _cacheDatabase = new Lazy<IDatabase>(() =>
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RedisCacheConnectionString"].ConnectionString;
            return ConnectionMultiplexer.Connect(connectionString).GetDatabase();
        });

        public async Task<bool> StoreAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var serializedValue = Serialize(value);
            var result = await _cacheDatabase.Value.StringSetAsync(key, serializedValue);
            await _cacheDatabase.Value.KeyExpireAsync(key, expiry, CommandFlags.FireAndForget);
            return result;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var serializedValue = await _cacheDatabase.Value.StringGetAsync(key);
            if (string.IsNullOrEmpty(serializedValue))
            {
                return default(T);
            }
            return Deserialize<T>(serializedValue);
        }
        public async Task<bool> DeleteAsync(string key)
        {
            return await _cacheDatabase.Value.KeyDeleteAsync(key, CommandFlags.FireAndForget);
        }

        private static byte[] Serialize<T>(T value)
        {
            byte[] objectDataAsStream = null;
            if (value != null)
            {
                var binaryFormatter = new BinaryFormatter();
                using (var memoryStream = new MemoryStream())
                {
                    binaryFormatter.Serialize(memoryStream, value);
                    objectDataAsStream = memoryStream.ToArray();
                }
            }
            return objectDataAsStream;
        }

        private static T Deserialize<T>(byte[] stream)
        {
            var result = default(T);
            if (stream != null)
            {
                var binaryFormatter = new BinaryFormatter();
                using (var memoryStream = new MemoryStream(stream))
                {
                    result = (T)binaryFormatter.Deserialize(memoryStream);
                }
            }

            return result;
        }
    }
}
