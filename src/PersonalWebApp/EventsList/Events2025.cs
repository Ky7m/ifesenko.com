using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList;

internal static class Events2025
{
    public static EventModel[] List { get; } =
    [
        new()
        {
            Title = "Svitla Smart Talk",
            Link = "https://kommunity.com/svitla-systems/events/svitla-smart-talk-building-nextgen-ai-apps-on-net-b87ff85d",
            Items =
            [
                new EventModelItem("Building Next‑Gen AI Apps on .NET",
                    new Dictionary<string, string>
                    {
                        ["https://github.com/Ky7m/DemoCode/tree/main/NextGenAIApps"] = CommonStrings.DemoCode,
                        ["https://www.youtube.com/watch?v=dxJmE9euahI"] = CommonStrings.Recording
                    })
            ],
            Location = CommonStrings.Online,
            Date = new DateTime(2025, 11, 27)
        },
        new()
        {
            Title = "Microsoft Ignite 2025",
            Link = "https://ignite.microsoft.com/en-US/home",
            Items =
            [
                new EventModelItem("App platform capabilities in AI Foundry", new Dictionary<string, string>()),
                new EventModelItem("Visual Studio and Visual Studio Code", new Dictionary<string, string>()),
                new EventModelItem("Agentic DevOps", new Dictionary<string, string>())
            ],
            Location = CommonStrings.SanFrancisco,
            Date = new DateTime(2025, 11, 17)
        },
        new()
        {
            Title = ".NET Conf 2025 Ukraine",
            Link = "https://app.softserveinc.com/apply/register/en/net-conf",
            Items =
            [
                new EventModelItem("Panel Discussion")
            ],
            Location = CommonStrings.Lviv,
            Date = new DateTime(2025, 11, 10)
        },
    ];
}