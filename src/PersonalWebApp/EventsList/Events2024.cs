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
            Date = new DateTime(2024, 12, 3)
        },
        new()
        {
            Title = "Microsoft Ignite 2024",
            Link = "https://ignite.microsoft.com/en-US/home",
            Items =
            [
                new EventModelItem("LAB: Integrate GenAI capabilities into your .NET apps with minimal code changes",
                    new Dictionary<string, string>
                    {
                        ["https://github.com/Azure-Samples/Ignite2024-Lab411/tree/main"] = CommonStrings.DemoCode
                    }),
                new EventModelItem("LAB: Put the guardrail in 'self-serve with guardrails' with Microsoft Dev Box",
                    new Dictionary<string, string>
                    {
                        ["https://devblogs.microsoft.com/develop-from-the-cloud/devboxignite2024/?wt.mc_id=DT-MVP-5002442"] = CommonStrings.DemoCode
                    })
            ],
            Location = CommonStrings.Chicago,
            Date = new DateTime(2024, 11, 19)
        },
        new()
        {
            Title = ".NET Conference 2024: Watch, Discuss, Network",
            Link = "https://share.hsforms.com/1L915qzXNQO-e0ANUEjX2WA3ir33",
            Items =
            [
                new EventModelItem("Panel Discussion")
            ],
            Location = CommonStrings.Online,
            Date = new DateTime(2024, 11, 12)
        },
        new()
        {
            Title = "One Line Coding Contest",
            Link = "https://robotdreams.cc/uk/course/1645-konkurs-na-naykrashchiy-laynokod-ukrajini",
            Items =
            [
                new EventModelItem("Expert Committee Member",
                    new Dictionary<string, string>())
            ],
            Location = CommonStrings.Online,
            Date = new DateTime(2024, 10, 31)
        },
        new()
        {
            Title = "Microsoft Build 2024",
            Link = "https://news.microsoft.com/build-2024-book-of-news/",
            Items =
            [
                new EventModelItem("Expert Meet-up .NET for Developer Tools",
                    new Dictionary<string, string>())
            ],
            Location = CommonStrings.Seattle,
            Date = new DateTime(2024, 5, 21)
        },
        new()
        {
            Title = "DevOps fwdays'24",
            Link = "https://fwdays.com/en/event/devops-fwdays-2024/review/platform-engineering-with-development-containers",
            Items =
            [
                new EventModelItem("Platform Engineering with Development Containers",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUn5tuQVnsfYfoAcD5Zw?e=w0nAqg"] = CommonStrings.Powerpoint,
                        ["https://github.com/Ky7m/DemoCode/blob/main/WorkWithTelemetryInDotNET/.devcontainer/devcontainer.json"] = CommonStrings.DemoCode,
                        ["https://www.youtube.com/watch?v=yBwtNDicNZ0"] = CommonStrings.Recording
                    })
            ],
            Location = CommonStrings.Online,
            Date = new DateTime(2024, 2, 17)
        }
    ];
}