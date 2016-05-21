using System;
using System.Threading.Tasks;

namespace ifesenko.com.Infrastructure.Services.Interfaces
{
    public interface ICacheService
    {
        Task<bool> StoreAsync<T>(string key, T value, TimeSpan? expiry = null) where T : class;
        Task<T> GetAsync<T>(string key);
        Task<bool> DeleteAsync(string key);
    }
}
