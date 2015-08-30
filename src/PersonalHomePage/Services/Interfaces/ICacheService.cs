using System;

namespace PersonalHomePage.Services.Interfaces
{
    public interface ICacheService
    {
        bool Store<T>(string key, T value, TimeSpan? expiry = null);
        T Get<T>(string key);
        bool Delete(string key);
    }
}
