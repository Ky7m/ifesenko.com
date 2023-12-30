using System;
using JetBrains.Annotations;
using PersonalWebApp.EventsList;
using PersonalWebApp.Models;

namespace PersonalWebApp.Services;

[UsedImplicitly]
public sealed class InMemoryStorageService : IStorageService
{
    public (EventModel[] Events, bool IsItAllEvents) RetrieveEventsForPeriod(string period)
    {
        if (string.Equals(period, "all", StringComparison.OrdinalIgnoreCase))
        {
            return (GetAllEvents(), true);
        }

        if (int.TryParse(period, out var year) && year >= 2015 && year <= DateTimeOffset.UtcNow.Year)
        {
            switch (year)
            {
                case 2015:
                    return (Events2015.List, false);
                case 2016:
                    return (Events2016.List, false);
                case 2017:
                    return (Events2017.List, false);
                case 2018:
                    return (Events2018.List, false);
                case 2019:
                    return (Events2019.List, false); 
                case 2020:
                    return (Events2020.List, false); 
                case 2021:
                    return (Events2021.List, false);
                case 2022:
                    return (Events2022.List, false);
                case 2023:
                    return (Events2023.List, false);
            }
        }
            
        return (Events2024.List, false);
    }

    private static EventModel[] GetAllEvents() =>
    [
        ..Events2024.List,
        ..Events2023.List,
        ..Events2022.List,
        ..Events2021.List,
        ..Events2020.List,
        ..Events2019.List,
        ..Events2018.List,
        ..Events2017.List,
        ..Events2016.List,
        ..Events2015.List
    ];
}