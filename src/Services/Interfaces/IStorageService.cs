using System.Threading.Tasks;
using ifesenko.com.Models;
using ifesenko.com.Services.Implementation.CloudStorageService.Model;

namespace ifesenko.com.Services.Interfaces
{
    public interface IStorageService
    {
        Task<SettingTableEntity[]> RetrieveAllSettingsForServiceAsync(string serviceName);
        Task ReplaceSettingValueForServiceAsync(SettingTableEntity updateSettingTableEntity);
        Task<ShortToLongUrlMapTableEntity> RetrieveLongUrlMapForShortUrlAsync(string shortUrl);
        Task<EventModel[]> RetrieveAllEventsAsync();
    }
}
