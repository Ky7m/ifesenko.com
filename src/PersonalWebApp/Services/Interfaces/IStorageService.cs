using PersonalWebApp.Services.Implementation.CloudStorageService.Model;

namespace PersonalWebApp.Services.Interfaces
{
    public interface IStorageService
    {
        (EventModel[] Events, bool IsItAllEvents) RetrieveEventsForPeriod(string period);
    }
}
