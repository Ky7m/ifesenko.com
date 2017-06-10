using PersonalWebApp.Models;

namespace PersonalWebApp.Services
{
    public interface IStorageService
    {
        (EventModel[] Events, bool IsItAllEvents) RetrieveEventsForPeriod(string period);
    }
}
