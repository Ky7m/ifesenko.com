using System;
using System.Configuration;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace PersonalHomePage.Services
{
    public sealed class RedisCacheService
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(() =>
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RedisCacheConnectionString"].ConnectionString;
            return ConnectionMultiplexer.Connect(connectionString);
        });

        public async Task<bool> StoreAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            return await _connectionMultiplexer.Value.GetDatabase().StringSetAsync(key, serializedValue, expiry);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var serializedValue = await _connectionMultiplexer.Value.GetDatabase().StringGetAsync(key);
            if (string.IsNullOrEmpty(serializedValue))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(serializedValue);
        }
        public async Task<bool> DeleteAsync(string key)
        {
            return await _connectionMultiplexer.Value.GetDatabase().KeyDeleteAsync(key);
        }
    }
}
