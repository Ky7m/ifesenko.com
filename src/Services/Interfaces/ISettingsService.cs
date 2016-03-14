using System.Collections.Generic;
using System.Threading.Tasks;
using ifesenko.com.Services.Implementation.CloudStorageService.Model;

namespace ifesenko.com.Services.Interfaces
{
    public interface ISettingsService
    {
        Task<Dictionary<string, SettingTableEntity>> RetrieveAllSettingsValuesForServiceAsync(string serviceName);
        Task ReplaceSettingValueForServiceAsync(string serviceName, string settingName, string settingValue);
    }
}
