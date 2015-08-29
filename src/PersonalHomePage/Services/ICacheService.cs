using System;

namespace PersonalHomePage.Services
{
    public interface ICacheService
    {
        bool Store<T>(string key, T value, TimeSpan? expiry = null);
        T Get<T>(string key);
        bool Delete(string key);
    }
}
