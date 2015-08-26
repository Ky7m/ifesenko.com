using System;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

        public bool Store<T>(string key, T value, TimeSpan? expiry = null)
        {
            var serializedValue = Serialize(value);
            return _cacheDatabase.Value.StringSet(key, serializedValue, expiry, flags: CommandFlags.FireAndForget);
        }

        public T Get<T>(string key)
        {
            if (!_cacheDatabase.Value.KeyExists(key))
            {
                return default(T);
            }
            var serializedValue = _cacheDatabase.Value.StringGet(key);
            if (!string.IsNullOrEmpty(serializedValue))
            {
                return Deserialize<T>(serializedValue);
            }
            return default(T);
        }
        public bool Delete(string key)
        {
            return _cacheDatabase.Value.KeyDelete(key, CommandFlags.FireAndForget);
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
