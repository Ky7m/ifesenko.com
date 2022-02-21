using System;
using System.Collections.Generic;
using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList;

internal static class Events2020
{
    public static EventModel[] List { get; } =
    {
        new()
        {
            Title = "Svitla Smart Talk: Advanced Debugging Tools & Techniques for C# Developers",
            Link = "https://www.facebook.com/events/3298952870202678",
            Items = new[]
            {
                new EventModelItem("Advanced Debugging Tools & Techniques for C# Developers",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUkNZlVizBwGnhmdt0bg?e=adTtGi"] = CommonStrings.Powerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/master/DebuggingScenarios"] = CommonStrings.DemoCode
                    })
            },
            Location = CommonStrings.Online,
            Date = new DateTime(2020, 11, 30)
        },
        new()
        {
            Title = ".NET Conference 2020",
            Link = "https://softserveinc.events/netconference2020",
            Items = new[]
            {
                new EventModelItem(".NET Development Yesterday, Today and Tomorrow",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUkNZi_3F1g6JWDFr4-Q?e=xKXzqz"] = CommonStrings.Powerpoint
                    })
            },
            Location = CommonStrings.Online,
            Date = new DateTime(2020, 11, 17)
        },
        new()
        {
            Title = "IT Arena 2020",
            Link = "https://itarena.ua/speaker/igor-fesenko/",
            Items = new[]
            {
                new EventModelItem("Tips From The Trenches: How to survive in Software Engineering",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUkNVtiKHEvokeU0wEbQ?e=bJv1XC"] = CommonStrings.Powerpoint
                    })
            },
            Location = CommonStrings.Online,
            Date = new DateTime(2020, 10, 10)
        },
        new()
        {
            Title = "Host and Deploy .NET Core App with Microsoft Azure",
            Link = "https://www.facebook.com/events/708950909939787/",
            Items = new[]
            {
                new EventModelItem("Host and Deploy .NET Core App with Microsoft Azure",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUkNJcTRQssiMkrsOgOQ?e=nhLOVs"] = CommonStrings.Powerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/master/PulumiDemo"] = CommonStrings.DemoCode,
                        ["https://www.facebook.com/SvitlaSystems/videos/vb.149447525111721/323925932064637/?type=3&theater"] = CommonStrings.Recording
                    })
            },
            Location = CommonStrings.Online,
            Date = new DateTime(2020, 7, 30)
        },
        new()
        {
            Title = "Podcast DotNet&More: .Net Summit",
            Link = "https://anchor.fm/dotnetmore/episodes/35---DotNetMore--Net-Summit-eggr6b",
            Items = new[]
            {
                new EventModelItem("Podcast DotNet&More: .Net Summit",
                    new Dictionary<string, string>
                    {
                        ["https://anchor.fm/dotnetmore/episodes/35---DotNetMore--Net-Summit-eggr6b"] = CommonStrings.Recording
                    })
            },
            Location = CommonStrings.Online,
            Date = new DateTime(2020, 7, 7)
        },
        new()
        {
            Title = "Diagnostics, instrumenting .NET Core 3.x application",
            Link = "https://www.facebook.com/events/2971745402871621/",
            Items = new[]
            {
                new EventModelItem("Diagnostics, instrumenting .NET Core 3.x application",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUkM0VMMrKi-6tYt3TZQ?e=x7lTKu"] = CommonStrings.Powerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/master/ApplicationDiagnosticsNetCore"] = CommonStrings.DemoCode,
                        ["https://www.facebook.com/149447525111721/videos/180335746489420/"] = CommonStrings.Recording
                    })
            },
            Location = CommonStrings.Online,
            Date = new DateTime(2020, 5, 7)
        },
        new()
        {
            Title = "Lifting Your Code Quality Higher",
            Link = "https://www.facebook.com/events/249165142884214/",
            Items = new[]
            {
                new EventModelItem("Lifting Your C# Code Quality Higher",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUkMVbbthLSqDt6fih9w?e=8cNVFf"] = CommonStrings.Powerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/master/LiftingYourCodeQualityHigher"] = CommonStrings.DemoCode
                    })
            },
            Location = CommonStrings.Online,
            Date = new DateTime(2020, 4, 23)
        },
        new()
        {
            Title = ".NET fwdays'20",
            Link = "https://fwdays.com/event/dotnet-fwdays-2020",
            Items = new[]
            {
                new EventModelItem("C# 8: Be Good, Get Good or Give Up",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUkMxy2UQ0W3oPza9TCA?e=SXyMh0f"] = CommonStrings.Powerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/master/CSharp8"] = CommonStrings.DemoCode,
                        ["https://www.youtube.com/watch?v=YB6Cl-UOYuQ"] = CommonStrings.Recording
                    })
            },
            Location = CommonStrings.Kyiv,
            Date = new DateTime(2020, 4, 11)
        },
        new()
        {
            Title = "Svitla Smart Talk: .NET. Lifting Your Code Quality Higher",
            Link = "https://www.facebook.com/events/191429968902776/",
            Items = new[]
            {
                new EventModelItem("Lifting Your C# Code Quality Higher",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUkMVbbthLSqDt6fih9w?e=8cNVFf"] = CommonStrings.Powerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/master/LiftingYourCodeQualityHigher"] = CommonStrings.DemoCode
                    })
            },
            Location = CommonStrings.Kyiv,
            Date = new DateTime(2020, 2, 27)
        }
    };
}