using System.Collections.Generic;
using PersonalHomePage.Services.Implementation.CloudStorageService.Model;

namespace PersonalHomePage.Services.Interfaces
{
    public interface ISettingsService
    {
        Dictionary<string, string> RetrieveAllSettingsValuesForService(string serviceName);

        void ReplaceSettingValueForService(string serviceName, string settingName, string settingValue);

        ShortToLongUrlMapTableEntity RetrieveLongUrlMapForShortUrl(string shortUrl);

    }
}
