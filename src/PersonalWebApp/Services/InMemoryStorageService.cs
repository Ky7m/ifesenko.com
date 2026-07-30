using PersonalWebApp.EventsList;
using PersonalWebApp.Models;

namespace PersonalWebApp.Services;

public sealed class InMemoryStorageService : IStorageService
{
    public (EventModel[] Events, bool IsItAllEvents, string ResolvedPeriod) RetrieveEventsForPeriod(string period)
    {
        if (string.Equals(period, "all", StringComparison.OrdinalIgnoreCase))
        {
            return (_allEvents.Values.SelectMany(x => x).ToArray(), true, "all");
        }

        if (int.TryParse(period, out var year) && _allEvents.TryGetValue(year, out var events))
        {
            return (events, false, year.ToString());
        }

        var currentYear = DateTime.UtcNow.Year;
        return _allEvents.TryGetValue(currentYear, out var currentYearEvents)
            ? (currentYearEvents, false, currentYear.ToString())
            : ([], false, currentYear.ToString());
    }

    private readonly Dictionary<int, EventModel[]> _allEvents = new()
    {
        [2026] = Events2026.List,
        [2025] = Events2025.List,
        [2024] = Events2024.List,
        [2023] = Events2023.List,
        [2022] = Events2022.List,
        [2021] = Events2021.List,
        [2020] = Events2020.List,
        [2019] = Events2019.List,
        [2018] = Events2018.List,
        [2017] = Events2017.List,
        [2016] = Events2016.List,
        [2015] = Events2015.List
    };
}