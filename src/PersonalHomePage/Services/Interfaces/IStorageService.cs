using System.Threading.Tasks;
using PersonalHomePage.Models;
using PersonalHomePage.Services.Implementation.CloudStorageService.Model;

namespace PersonalHomePage.Services.Interfaces
{
    public interface IStorageService
    {
        Task<SettingTableEntity[]> RetrieveAllSettingsForServiceAsync(string serviceName);
        Task ReplaceSettingValueForServiceAsync(SettingTableEntity updateSettingTableEntity);
        Task<ShortToLongUrlMapTableEntity> RetrieveLongUrlMapForShortUrlAsync(string shortUrl);
        Task<EventModel[]> RetrieveAllEventsAsync();
    }
}
