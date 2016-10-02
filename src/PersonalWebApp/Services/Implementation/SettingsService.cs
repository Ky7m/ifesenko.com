using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalWebApp.Services.Implementation.CloudStorageService.Model;
using PersonalWebApp.Services.Interfaces;

namespace PersonalWebApp.Services.Implementation
{
    public sealed class SettingsService : ISettingsService
    {
        private readonly IStorageService _storageService;

        public SettingsService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<Dictionary<string, SettingTableEntity>> RetrieveAllSettingsValuesForServiceAsync(string serviceName)
        {
            var settings = await _storageService.RetrieveAllSettingsForServiceAsync(serviceName);
            var settingsValues = new Dictionary<string, SettingTableEntity>(settings.Length);

            foreach (var settingTableEntity in settings)
            {
                settingsValues.Add(settingTableEntity.RowKey, settingTableEntity);
            }

            return settingsValues;
        }
        public async Task ReplaceSettingValueForServiceAsync(string serviceName, string settingName, string settingValue)
        {
            var updateSettingTableEntity = new SettingTableEntity(serviceName, settingName)
            {
                Value = settingValue
            };

            await _storageService.ReplaceSettingValueForServiceAsync(updateSettingTableEntity);
        }
    }
}
