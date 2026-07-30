using PersonalWebApp.Models;

namespace PersonalWebApp.Services;

public interface IStorageService
{
    (EventModel[] Events, bool IsItAllEvents, string ResolvedPeriod) RetrieveEventsForPeriod(string period);
}