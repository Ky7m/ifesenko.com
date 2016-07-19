using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalWebApp.Infrastructure.Services.Implementation.CloudStorageService.Model;

namespace PersonalWebApp.Infrastructure.Services.Interfaces
{
    public interface ISettingsService
    {
        Task<Dictionary<string, SettingTableEntity>> RetrieveAllSettingsValuesForServiceAsync(string serviceName);
        Task ReplaceSettingValueForServiceAsync(string serviceName, string settingName, string settingValue);
    }
}
