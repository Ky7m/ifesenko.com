using System;
using System.Collections.Generic;
using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList;

internal static class Events2023
{
    public static EventModel[] List { get; } = {
        new()
        {
            Title = ".NET Apps Observability with OpenTelemetry",
            Link = "https://www.linkedin.com/feed/update/urn:li:activity:7127654191111487489/",
            Items = new[]
            {
                new EventModelItem(".NET Apps Observability with OpenTelemetry",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUn5gSjgrUggOjASjzow?e=NySE5v"] = CommonStrings.Powerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/main/WorkWithTelemetryInDotNET"] = CommonStrings.DemoCode
                    })
            },
            Location = CommonStrings.Online,
            Date = new DateTime(2023, 11, 17)
        },
        new()
        {
            Title = ".NET Conf 2023: Watch, Discuss, Network",
            Link = "https://app.softserveinc.com/apply/register/en/net-conf-2023-1",
            Items = new[]
            {
                new EventModelItem("Panel Discussion")
            },
            Location = CommonStrings.Lviv,
            Date = new DateTime(2023, 11, 14)
        },
        new()
        {
            Title = ".NET fwdays'23",
            Link = "https://fwdays.com/en/event/dotnet-fwdays-2023",
            Items = new[]
            {
                new EventModelItem("Discussion with Kevin Gosse",
                    new Dictionary<string, string>
                    {
                        ["https://fwdays.com/en/event/dotnet-fwdays-2023/review/discussion-with-kevin-gosse"] = CommonStrings.Online
                    })
            },
            Location = CommonStrings.Online,
            Date = new DateTime(2023, 6, 15)
        },
        new()
        {
            Title = "Microsoft Build 2023",
            Link = "https://news.microsoft.com/build-2023/",
            Items = new[]
            {
                new EventModelItem("LAB: Build a serverless web application end-to-end on Microsoft Azure",
                    new Dictionary<string, string>
                    {
                        ["https://learn.microsoft.com/en-us/users/learn-live/collections/12odt3rkw1nqnd?wt.mc_id=DT-MVP-5002442"] = CommonStrings.Online
                    }),
                new EventModelItem("Expert Meet-up .NET",
                    new Dictionary<string, string>())
            },
            Location = CommonStrings.Seattle,
            Date = new DateTime(2023, 5, 23)
        },
        new()
        {
            Title = "DevOps fwdays'23 conference",
            Link = "https://fwdays.com/en/event/devops-fwdays-2023/review/modern-devops-real-life-applications",
            Items = new[]
            {
                new EventModelItem("Modern DevOps & Real Life Applications. 3.0.0-devops+20230318",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUkPxHesyZdfXmSQZLcg?e=JZsyD3"] = CommonStrings.Powerpoint,
                        ["https://www.youtube.com/watch?v=BQ7QlQqfcOw"] = CommonStrings.Recording
                    })
            },
            Location = CommonStrings.Online,
            Date = new DateTime(2023, 3, 18)
        }
    };
}