using PersonalHomePage.Services.CloudStorageService.Model;

namespace PersonalHomePage.Services.CloudStorageService
{
    public interface IStorageService
    {
        SettingTableEntity[] RetrieveAllSettingsForService(string serviceName);

        void ReplaceSettingValueForService(SettingTableEntity updateSettingTableEntity);
    }
}
