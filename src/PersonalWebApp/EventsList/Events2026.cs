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
            Link = "https://kommunity.com/svitla-systems/events/a-day-in-the-life-of-a-software-engineer-how-c-helps-them-succeed-61b35bfd",
            Items =
            [
                new EventModelItem("A Day in the Life of a Software Engineer: How C# Helps Them Succeed",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUoaQ-KUT32UVnbYZVSg?e=eEbQli"] = CommonStrings.Powerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/main/SvitlaSmartTalk2024"] = CommonStrings.DemoCode,
                        ["https://www.youtube.com/watch?v=tFh5VgWfZuU"] = CommonStrings.Recording
                    })
            ],
            Location = CommonStrings.Online,
            Date = new DateTime(2026, 12, 3)
        }
    ];
}