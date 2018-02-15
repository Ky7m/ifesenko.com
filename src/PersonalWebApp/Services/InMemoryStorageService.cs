using System;
using System.Linq;
using JetBrains.Annotations;
using PersonalWebApp.EventsList;
using PersonalWebApp.Models;

namespace PersonalWebApp.Services
{
    [UsedImplicitly]
    public sealed class InMemoryStorageService : IStorageService
    {
        public (EventModel[] Events, bool IsItAllEvents) RetrieveEventsForPeriod(string period)
        {
            if (string.Equals(period, "all", StringComparison.OrdinalIgnoreCase))
            {
                return (GetAllEvents(), true);
            }

            if (int.TryParse(period, out var year) && year >= 2015 && year < DateTime.UtcNow.Year)
            {
                switch (year)
                {
                    case 2015:
                        return (Events2015.List, false);
                    case 2016:
                        return (Events2016.List, false);
                    case 2017:
                        return (Events2017.List, false);
                }
            }
            
            return (Events2018.List, false);
        }

        private static EventModel[] GetAllEvents() => Events2018.List
            .Concat(Events2017.List)
            .Concat(Events2016.List)
            .Concat(Events2015.List)
            .ToArray();
    }
}
