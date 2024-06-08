using System;
using System.Collections.Generic;
using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList;

internal static class Events2024
{
    public static EventModel[] List { get; } =
    [
        new()
        {
            Title = "Microsoft Build 2024",
            Link = "https://news.microsoft.com/build-2024-book-of-news/",
            Items = new[]
            {
                new EventModelItem("Expert Meet-up .NET for Developer Tools",
                    new Dictionary<string, string>())
            },
            Location = CommonStrings.Seattle,
            Date = new DateTime(2024, 5, 21)
        },
        new()
        {
            Title = "DevOps fwdays'24",
            Link = "https://fwdays.com/en/event/devops-fwdays-2024/review/platform-engineering-with-development-containers",
            Items = new[]
            {
                new EventModelItem("Platform Engineering with Development Containers",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUn5tuQVnsfYfoAcD5Zw?e=w0nAqg"] = CommonStrings.Powerpoint,
                        ["https://github.com/Ky7m/DemoCode/blob/main/WorkWithTelemetryInDotNET/.devcontainer/devcontainer.json"] = CommonStrings.DemoCode
                    })
            },
            Location = CommonStrings.Online,
            Date = new DateTime(2024, 2, 17)
        }
    ];
}