using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalWebApp.Services.Implementation.CloudStorageService.Model;

namespace PersonalWebApp.Services.Interfaces
{
    public interface ISettingsService
    {
        Task<Dictionary<string, SettingTableEntity>> RetrieveAllSettingsValuesForServiceAsync(string serviceName);
        Task ReplaceSettingValueForServiceAsync(string serviceName, string settingName, string settingValue);
    }
}
