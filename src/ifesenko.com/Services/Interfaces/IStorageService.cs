using System.Threading.Tasks;
using IfesenkoDotCom.Models;
using IfesenkoDotCom.Services.Implementation.CloudStorageService.Model;

namespace IfesenkoDotCom.Services.Interfaces
{
    public interface IStorageService
    {
        Task<SettingTableEntity[]> RetrieveAllSettingsForServiceAsync(string serviceName);
        Task ReplaceSettingValueForServiceAsync(SettingTableEntity updateSettingTableEntity);
        Task<ShortToLongUrlMapTableEntity> RetrieveLongUrlMapForShortUrlAsync(string shortUrl);
        Task<EventModel[]> RetrieveAllEventsAsync();
    }
}
