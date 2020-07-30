using System;
using System.Collections.Generic;
using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList
{
    internal static class Events2020
    {
        public static EventModel[] List { get; } =
        {
            new EventModel
            {
                Title = "Host and Deploy .NET Core App with Microsoft Azure",
                Link = "https://www.facebook.com/events/708950909939787/",
                Items = new[]
                {
                    new EventModelItem("Host and Deploy .NET Core App with Microsoft Azure",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkNJcTRQssiMkrsOgOQ?e=nhLOVs"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/PulumiDemo"] = CommonStrings.DemoCode
                        })
                },
                Location = CommonStrings.Webcast,
                Date = new DateTime(2020, 7, 30)
            },
            new EventModel
            {
                Title = "Podcast DotNet&More: .Net Summit",
                Link = "https://dotnetmore.ru/podcast/35-netsummit/",
                Items = new[]
                {
                    new EventModelItem("Podcast DotNet&More: .Net Summit",
                        new Dictionary<string, string>
                        {
                            ["https://dotnetmore.ru/wp-content/uploads/2020/07/DotNetAndMore-35-DotNetSummit.mp3"] = CommonStrings.Webcast
                        })
                },
                Location = CommonStrings.Webcast,
                Date = new DateTime(2020, 7, 7)
            },
            new EventModel
            {
                Title = "Diagnostics, instrumenting .NET Core 3.x application",
                Link = "https://www.facebook.com/events/2971745402871621/",
                Items = new[]
                {
                    new EventModelItem("Diagnostics, instrumenting .NET Core 3.x application",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkM0VMMrKi-6tYt3TZQ?e=x7lTKu"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/ApplicationDiagnosticsNetCore"] = CommonStrings.DemoCode
                        })
                },
                Location = CommonStrings.Webcast,
                Date = new DateTime(2020, 5, 7)
            },
            new EventModel
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
                Location = CommonStrings.Webcast,
                Date = new DateTime(2020, 4, 23)
            },
            new EventModel
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
                            ["https://www.youtube.com/watch?v=YB6Cl-UOYuQ"] = CommonStrings.VideoRus
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2020, 4, 11)
            },
            new EventModel
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
}