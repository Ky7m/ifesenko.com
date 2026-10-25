using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList;

internal static class Events2026
{
    public static EventModel[] List { get; } =
    [
        new()
        {
            Title = "Svitla Smart Talk",
            Link = "https://kommunity.com/svitla-systems/events/svitla-smart-talk-agentic-engineering-for-net-developers-lessons-learned-ecd328a1",
            Items =
            [
                new EventModelItem("Agentic Engineering for .NET Developers: Lessons Learned",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/c/06f653caf3f7be62/IQA0S_LdRWT3RbDkjkugBz93AQPflhdOBVcOmas1_i-xh2s?e=mnpV8V"] = CommonStrings.Powerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/main/TokenOps"] = CommonStrings.DemoCode,
                        ["https://www.youtube.com/watch?v=3R60fO-enNw"] = CommonStrings.Recording
                    })
            ],
            Location = CommonStrings.Online,
            Date = new DateTime(2026, 10, 15)
        },
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