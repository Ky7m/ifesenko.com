using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalHomePage.Services.Implementation.CloudStorageService.Model;

namespace PersonalHomePage.Services.Interfaces
{
    public interface ISettingsService
    {
        Task<Dictionary<string, SettingTableEntity>> RetrieveAllSettingsValuesForServiceAsync(string serviceName);
        Task ReplaceSettingValueForServiceAsync(string serviceName, string settingName, string settingValue);
    }
}
