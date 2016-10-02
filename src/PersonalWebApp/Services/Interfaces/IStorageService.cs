using System.Threading.Tasks;
using PersonalWebApp.Services.Implementation.CloudStorageService.Model;

namespace PersonalWebApp.Services.Interfaces
{
    public interface IStorageService
    {
        Task<SettingTableEntity[]> RetrieveAllSettingsForServiceAsync(string serviceName);
        Task ReplaceSettingValueForServiceAsync(SettingTableEntity updateSettingTableEntity);
        Task<ShortToLongUrlMapTableEntity> RetrieveLongUrlMapForShortUrlAsync(string shortUrl);
        Task<EventModel[]> RetrieveAllEventsAsync();
    }
}
