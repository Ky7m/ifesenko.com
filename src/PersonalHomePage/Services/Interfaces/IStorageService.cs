using PersonalHomePage.Services.Implementation.CloudStorageService.Model;

namespace PersonalHomePage.Services.Interfaces
{
    public interface IStorageService
    {
        SettingTableEntity[] RetrieveAllSettingsForService(string serviceName);

        void ReplaceSettingValueForService(SettingTableEntity updateSettingTableEntity);

        ShortToLongUrlMapTableEntity RetrieveLongUrlMapForShortUrl(string shortUrl);
    }
}
