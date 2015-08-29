using System.Collections.Generic;
using PersonalHomePage.Services.CloudStorageService;
using PersonalHomePage.Services.CloudStorageService.Model;

namespace PersonalHomePage.Services
{
    public sealed class SettingsService : ISettingsService
    {
        private readonly IStorageService _storageService;

        public SettingsService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public Dictionary<string, string> RetrieveAllSettingsValuesForService(string serviceName)
        {
            var settings = _storageService.RetrieveAllSettingsForService(serviceName);
            var settingsValues = new Dictionary<string,string>(settings.Length);

            foreach (var settingTableEntity in settings)
            {
                settingsValues.Add(settingTableEntity.RowKey,settingTableEntity.Value);
            }

            return settingsValues;
        }
        public void ReplaceSettingValueForService(string serviceName, string settingName, string settingValue)
        {
            var updateSettingTableEntity = new SettingTableEntity(serviceName, settingName)
            {
                Value = settingValue
            };

            _storageService.ReplaceSettingValueForService(updateSettingTableEntity);
        } 
    }
}
