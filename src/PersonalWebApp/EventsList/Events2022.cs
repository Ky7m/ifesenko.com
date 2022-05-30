using System;
using System.Collections.Generic;
using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList;

internal static class Events2022
{
    public static EventModel[] List { get; } =
    {
        new()
        {
            Title = "Svitla Smart Talk",
            Link = "https://kommunity.com/svitla-systems/events/svitla-smart-talk-top-things-we-do-wrong-with-telemetry-data-in-net-e5430dd0",
            Items = new []
            {
                new EventModelItem("Top Things We Do Wrong With Telemetry Data In .NET",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUkO5q4HoH_2zdjS75dA?e=tIh2bx"] = CommonStrings.Powerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/main/WorkWithTelemetryInDotNET"] = CommonStrings.DemoCode,
                        ["https://www.youtube.com/watch?v=LdYBE8ftw18"] = CommonStrings.Recording
                    })
            },
            Location = CommonStrings.Online,
            Date = new DateTime(2022, 5, 26)
        },
        new()
        {
            Title = "MS Stage Conference",
            Link = "https://msstage.com/speakers/igor-fesenko/",
            Items = new []
            {
                new EventModelItem("Top Things We Do Wrong With Telemetry Data In .NET",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUkO5q4HoH_2zdjS75dA?e=tIh2bx"] = CommonStrings.Powerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/main/WorkWithTelemetryInDotNET"] = CommonStrings.DemoCode,
                        ["https://www.youtube.com/watch?v=ICy2m5tL730"] = CommonStrings.Recording
                    })
            },
            Location = CommonStrings.Online,
            Date = new DateTime(2022, 4, 1)
        }
    };
}