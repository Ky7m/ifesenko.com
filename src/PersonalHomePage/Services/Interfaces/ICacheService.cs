using System;
using System.Threading.Tasks;

namespace PersonalHomePage.Services.Interfaces
{
    public interface ICacheService
    {
        Task<bool> StoreAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task<T> GetAsync<T>(string key);
        Task<bool> DeleteAsync(string key);
    }
}
