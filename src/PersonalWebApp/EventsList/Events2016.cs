using System;
using System.Collections.Generic;
using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList
{
    internal static class Events2016
    {
        public static EventModel[] List { get; } =
        {
            new()
            {
                Title = "Most JS Frameworks Day 2016",
                Link = "http://frameworksdays.com/event/most-js-fwdays-2016/review/lets-build-a-web-application",
                Items = new[]
                {
                    new EventModelItem("Let's Build a Web Application (and Talk About Ways to Improve Bad Parts)",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNgQofEVXN-RC-1fw"] = CommonStrings.Powerpoint,
                            ["https://www.youtube.com/watch?v=teQkX9GoV1g"] = CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2016, 12, 4)
            },
            new()
            {
                Title = "XP Days Ukraine",
                Link = "http://xpdays.com.ua/speaker/igor-fesenko",
                Items = new[]
                {
                    new EventModelItem("Continuous Learning using Application Performance Management & Monitoring",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNfNE_1PZAWuz27DA"] = CommonStrings.Powerpoint,
                            ["https://www.youtube.com/watch?v=1OZNgD_eIqU"] = CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2016, 11, 11)
            },
            new()
            {
                Title = "Metrics Morning@Lohika",
                Link = "http://morning.lohika.com/past-events/metrics-morning",
                Items = new[]
                {
                    new EventModelItem("Continuous Learning using Application Performance Management & Monitoring",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNbe6H8GU6_9ebEew"] = CommonStrings.Powerpoint,
                            ["https://www.youtube.com/watch?v=aFsk7yDkwBs"] = CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Lviv,
                Date = new DateTime(2016, 11, 5)
            },
            new()
            {
                Title = ".NET Framework Day 2016",
                Link = "http://frameworksdays.com/event/net-framework-day-2016/review/direction-of-csharp",
                Items = new[]
                {
                    new EventModelItem("Direction of C# as a High-Performance Language",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNakepnrf471XgHIQ"] = CommonStrings.Powerpoint,
                            ["https://www.youtube.com/watch?v=rf6tZVog6LE"] = CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2016, 10, 22)
            },
            new()
            {
                Title = "IT Weekend Ukraine 2016",
                Link = "https://ukraine.itweekend.ua/en/",
                Items = new[]
                {
                    new EventModelItem("Direction of C# as a High-Performance Language",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNX5JautJLHYTLU0w"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2016, 9, 17)
            },
            new()
            {
                Title = "AzureDay",
                Link = "http://azureday.net/",
                Items = new[]
                {
                    new EventModelItem("Workshop: Application Insights From The Ground Up",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNW8LO4YXCtdtYXIg"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/ApplicationInsightsWorkshop"] =
                                CommonStrings.DemoCode
                        }),
                    new EventModelItem("Enabling DevTest in Microsoft Azure",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNVjIxVov3ZsewJ9Q"] = CommonStrings.Powerpoint
                        }),
                    new EventModelItem("Channel 9: DevOps Video Lessons",
                        new Dictionary<string, string>
                        {
                            ["https://channel9.msdn.com/Shows/DevOpsUA/Enabling-DevTest-in-Azure"] =
                                CommonStrings.Recording,
                            ["https://channel9.msdn.com/Shows/DevOpsUA/Enabling-DevTest-in-Azure-demo"] =
                                CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2016, 6, 18)
            },
            new()
            {
                Title = "Application Innovation Strategy Briefing",
                Link = "https://www.microsoftevents.com/profile/form/index.cfm?PKformID=0x138568b7ed",
                Items = new[]
                {
                    new EventModelItem(
                        "The Application Innovation Strategy Briefing is a mix of discussion, application architecture design, white boarding and hands-on labs. Your team will design the future of business with new functionalities that augment existing business applications and drive development and execution of new initiatives.")
                },
                Location = "Cambridge, MA (USA)",
                Date = new DateTime(2016, 6, 9)
            },
            new()
            {
                Title = "Modernize your business - a Modern Requirements and Microsoft Showcase",
                Link = "https://www.microsoftevents.com/profile/form/index.cfm?PKformID=0x148353d22c",
                Items = new[]
                {
                    new EventModelItem(
                        "OPTIMIZE your business by streamlining application lifecycle management process")
                },
                Location = CommonStrings.SanFrancisco,
                Date = new DateTime(2016, 4, 28)
            },
            new()
            {
                Title = CommonStrings.DotNetCommunitySoftServe,
                Items = new[]
                {
                    new EventModelItem(".NET Framework 461 & C# 67",
                        new Dictionary<string, string>
                        {
                            ["https://www.youtube.com/watch?v=auSzzmaUTWU"] = CommonStrings.Recording,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNSDzCr6-mUwqWbmg"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Online,
                Date = new DateTime(2016, 4, 20)
            },
            new()
            {
                Title = "Ciklum Lviv .Net Saturday",
                Link = "http://dou.ua/calendar/10488/",
                Items = new[]
                {
                    new EventModelItem("The Present and Future of C# or I Know What You Did on Your Last Project",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNRKE6T2hmJ6C5FVA"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/CiklumLvivDotNetSaturday"] =
                                CommonStrings.DemoCode
                        })
                },
                Location = CommonStrings.Lviv,
                Date = new DateTime(2016, 4, 16)
            },
            new()
            {
                Title = CommonStrings.DotNetCommunitySoftServe,
                Items = new[]
                {
                    new EventModelItem("Exception Handling - Advanced Tips & Tricks",
                        new Dictionary<string, string>
                        {
                            ["https://www.youtube.com/watch?v=_Gw_9KeJlbg"] = CommonStrings.Recording,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNQ45EPNQN2UITD7g"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/ExceptionHandlingAdvancedTipsAndTricks"] =
                                CommonStrings.DemoCode
                        })
                },
                Location = CommonStrings.Online,
                Date = new DateTime(2016, 2, 24)
            },
            new()
            {
                Title = "Web UI Community - SoftServe",
                Items = new[]
                {
                    new EventModelItem("Web Development in Future",
                        new Dictionary<string, string>
                        {
                            ["https://www.youtube.com/watch?v=Om_NPxSuEf8"] = CommonStrings.Recording,
                            ["https://sway.com/LxEYWidCXvki-OHN"] = CommonStrings.Sway
                        })
                },
                Location = CommonStrings.Online,
                Date = new DateTime(2016, 2, 18)
            },
            new()
            {
                Title = ".Net Morning@Lohika",
                Link = "http://morning.lohika.com/past-events/net-morninglohika",
                Items = new[]
                {
                    new EventModelItem("Effective Memory Management: Memory Hygiene",
                        new Dictionary<string, string>
                        {
                            ["https://www.youtube.com/watch?v=vUX2wFciHrs"] = CommonStrings.Recording,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNFob8ClLkvBNX-fQ"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/EffectiveMemoryManagement"] =
                                CommonStrings.DemoCode
                        })
                },
                Location = CommonStrings.Lviv,
                Date = new DateTime(2016, 2, 13)
            },
            new()
            {
                Title = CommonStrings.DotNetCommunitySoftServe,
                Items = new[]
                {
                    new EventModelItem("How C# 6.0 Simplifies Your Code",
                        new Dictionary<string, string>
                        {
                            ["https://www.youtube.com/watch?v=gUfrK1rGWB0"] = CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Online,
                Date = new DateTime(2016, 2, 10)
            },
            new()
            {
                Title = "Enable Application Innovation Immersion",
                Link = "https://www.microsoftevents.com/profile/form/index.cfm?PKformID=0x40756dbc3",
                Items = new[]
                {
                    new EventModelItem(
                        "The Enable Application Innovation Immersion is a mix of discussion, application architecture design, white boarding and hands-on labs. Your team will design the future of business with new functionalities that augment existing business applications and drive development and execution of new initiatives.")
                },
                Location = "Dallas, TX (USA)",
                Date = new DateTime(2016, 1, 26)
            }
        };
    }
}