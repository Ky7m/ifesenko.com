using System.Threading.Tasks;
using PersonalWebApp.Services.Implementation.CloudStorageService.Model;

namespace PersonalWebApp.Services.Interfaces
{
    public interface IStorageService
    {
        ValueTask<EventModel[]> RetrieveAllEventsAsync();
    }
}
