using System.Collections.Generic;
using System.Threading.Tasks;
using IfesenkoDotCom.Services.Implementation.CloudStorageService.Model;

namespace IfesenkoDotCom.Services.Interfaces
{
    public interface ISettingsService
    {
        Task<Dictionary<string, SettingTableEntity>> RetrieveAllSettingsValuesForServiceAsync(string serviceName);
        Task ReplaceSettingValueForServiceAsync(string serviceName, string settingName, string settingValue);
    }
}
