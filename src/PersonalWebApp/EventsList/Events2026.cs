using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList;

internal static class Events2026
{
    public static EventModel[] List { get; } =
    [
        new()
        {
            Title = "Microsoft AI Tour - New York City 2026",
            Link = "https://aitour.microsoft.com/flow/microsoft/nyc26/landingpage/page/cityhome",
            Items =
            [
                new EventModelItem("Cloud and AI Platforms: Azure App Platform", new Dictionary<string, string>())
            ],
            Location = CommonStrings.NewYork,
            Date = new DateTime(2026, 1, 21)
        },
        new()
        {
            Title = "2026 Imagine Cup",
            Link = "https://imaginecup.microsoft.com/en-us",
            Items =
            [
                new EventModelItem("Technical Judge", new Dictionary<string, string>())
            ],
            Location = CommonStrings.Online,
            Date = new DateTime(2026, 1, 12)
        }
    ];
}