using System.Threading.Tasks;
using ifesenko.com.Infrastructure.Services.Implementation.CloudStorageService.Model;
using ifesenko.com.Models;

namespace ifesenko.com.Infrastructure.Services.Interfaces
{
    public interface IStorageService
    {
        Task<SettingTableEntity[]> RetrieveAllSettingsForServiceAsync(string serviceName);
        Task ReplaceSettingValueForServiceAsync(SettingTableEntity updateSettingTableEntity);
        Task<ShortToLongUrlMapTableEntity> RetrieveLongUrlMapForShortUrlAsync(string shortUrl);
        Task<EventModel[]> RetrieveAllEventsAsync();
    }
}
