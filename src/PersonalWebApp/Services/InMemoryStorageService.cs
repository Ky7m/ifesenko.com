using System;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.Services
{
    [UsedImplicitly]
    public sealed class InMemoryStorageService : IStorageService
    {
        public (EventModel[] Events, bool IsItAllEvents) RetrieveEventsForPeriod(string period)
        {
            if (string.Equals(period, "all", StringComparison.OrdinalIgnoreCase))
            {
                return (PopulateEvents(), true);
            }

            if (int.TryParse(period, out int year) && year >= 2015 && year < DateTime.UtcNow.Year)
            {
                switch (year)
                {
                    case 2015:
                        return (Events2015, false);
                    case 2016:
                        return (Events2016, false);
                }
            }
            
            return (Events, false);
        }


        private static EventModel[] PopulateEvents() => Events.Concat(Events2016).Concat(Events2015).ToArray();

        private static EventModel[] Events { get; } = {
            new EventModel
            {
                Title = "XP Days Ukraine",
                Link = "http://xpdays.com.ua/speaker/igor-fesenko-2/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("To be anounced...",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            //["https://1drv.ms/p/s!AmdJq1kgIxHUjuNfNE_1PZAWuz27DA"] = CommonStrings.CollateralPowerpoint,
                            //["https://www.youtube.com/watch?v=1OZNgD_eIqU"] = CommonStrings.CollateralVideoRus
                        })
                }),
                Location = "Kyiv (Ukraine)",
                Date = new DateTime(2017, 11, 10)
            },
            new EventModel
            {
                Title = ".Net Fest 2017",
                Link = "http://dotnetfest.com/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("C# Language Internals", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuMA0ujGpZEBr94v0Q"] = CommonStrings.CollateralPowerpoint
                    })
                }),
                Location = "Kyiv (Ukraine)",
                Date = new DateTime(2017, 10, 28)
            },
            new EventModel
            {
                Title = "AzureDAY 2017",
                Link = "https://azureday.net/Speaker/IFesenko",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Applied Microsoft Azure: Cloud Cost Optimization Techniques and Hacks For Enterprise Apps", new ImmutableDictionaryBuilder<string, string>
                    {
                        //["https://1drv.ms/p/s!AmdJq1kgIxHUjuNW8LO4YXCtdtYXIg"] = CommonStrings.CollateralPowerpoint,
                       //["https://github.com/Ky7m/DemoCode/tree/master/ApplicationInsightsWorkshop"] = CommonStrings.CollateralDemoCode
                    })
                }),
                Location = "Kyiv (Ukraine)",
                Date = new DateTime(2017, 9, 9)
            },
            new EventModel
            {
                Title = "LvivJS",
                Link = "http://lvivjs.org.ua/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("How to Track Success of Your Application", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjvN0CvlZT3FH9xDD3g"] = CommonStrings.CollateralPowerpoint
                        //["https://www.youtube.com/watch?v=teQkX9GoV1g"] = CommonStrings.CollateralVideoRus
                    })
                }),
                Location = "Lviv (Ukraine)",
                Date = new DateTime(2017, 8, 26)
            },
            new EventModel
            {
                Title = "Webinar “What’s New in C# 7.x”",
                Link = "https://dou.ua/calendar/16639/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("What’s New in C# 7.x", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjvNzrpzsPDMh46sePg"] = CommonStrings.CollateralPowerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/master/CSharp7"] = CommonStrings.CollateralDemoCode,
                        ["https://www.youtube.com/watch?v=zY0ELCJCcJQ"] = CommonStrings.CollateralVideoRus
                    })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2017, 7, 27)
            },
            new EventModel
            {
                Title = "IT Weekend Kharkiv: Enterprise Software Solutions",
                Link = "https://itweekend.ua/en/announcements/itw-kh-17m/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("How to Survive in Software Development", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNtEU4vkzhlKSdx3Q"] = CommonStrings.CollateralPowerpoint
                    })
                }),
                Location = "Kharkiv (Ukraine)",
                Date = new DateTime(2017, 5, 27)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("What’s New in C# 7.0", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNs5VwIZYHzyEqvwQ"] = CommonStrings.CollateralPowerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/master/CSharp7"] = CommonStrings.CollateralDemoCode,
                        ["https://www.youtube.com/watch?v=8aCQKnvJWZU"] = CommonStrings.CollateralVideoRus
                    })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2017, 5, 17)
            },
            new EventModel
            {
                Title = "IT Weekend Rivne",
                Link = "https://itweekend.ua/ua/announcements/itw-rv-17a/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("What’s New in C# 7.0", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNrcFT02PtgE55mxQ"] = CommonStrings.CollateralPowerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/master/CSharp7"] = CommonStrings.CollateralDemoCode
                    })
                }),
                Location = "Rivne (Ukraine)",
                Date = new DateTime(2017, 4, 29)
            },
            new EventModel
            {
                Title = "MeetUp “What’s New in C# 7.0”",
                Link = "https://www.facebook.com/events/1851330288440169/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("What’s New in C# 7.0", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNpSkpb8DlPChyCHA"] = CommonStrings.CollateralPowerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/master/CSharp7"] = CommonStrings.CollateralDemoCode
                    })
                }),
                Location = "Lviv (Ukraine)",
                Date = new DateTime(2017, 4, 26)
            },
            new EventModel
            {
                Title = "MUG Dnipro: C# 7.0 & API",
                Link = "https://www.facebook.com/events/1874373382806547/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("What’s New in C# 7.0", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNpSkpb8DlPChyCHA"] = CommonStrings.CollateralPowerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/master/CSharp7"] = CommonStrings.CollateralDemoCode,
                        ["https://www.youtube.com/watch?v=PL6GBQBf9-I&t=4m"] = CommonStrings.CollateralVideoRus
                    })
                }),
                Location = "Dnipropetrovsk (Ukraine)",
                Date = new DateTime(2017, 4, 22)
            },

            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Cake (C# Make)", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNogknwMLZZSQmfiA"] = CommonStrings.CollateralPowerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/master/CakeBuild"] = CommonStrings.CollateralDemoCode,
                        ["https://www.youtube.com/watch?v=1KQOUzY555E"] = CommonStrings.CollateralVideoRus
                    })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2017, 4, 19)
            },
            new EventModel
            {
                Title = "TalkIT Ivano-Frankivsk #.Net",
                Link = "https://www.facebook.com/events/200151533802892/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Why You Should Take Another Look at C# in 2017", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNnuN2194rbhhrwOg"] = CommonStrings.CollateralPowerpoint
                    })
                }),
                Location = "Ivano-Frankivsk (Ukraine)",
                Date = new DateTime(2017, 4, 1)
            },
            new EventModel
            {
                Title = ".NET Framework Day 2017",
                Link = "http://frameworksdays.com/event/dotnet-fwdays-2017/review/what-is-new-in-c-sharp-7-0",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("What’s New in C# 7.0", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNm65LELY2o3pegng"] = CommonStrings.CollateralPowerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/master/CSharp7"] = CommonStrings.CollateralDemoCode,
                        ["https://www.youtube.com/watch?v=n3PkTM32DCY"] = CommonStrings.CollateralVideoRus
                    })
                }),
                Location = "Kyiv (Ukraine)",
                Date = new DateTime(2017, 3, 25)
            },
            new EventModel
            {
                Title = "Rivne .NET Community inaugural meeting",
                Link = "https://www.facebook.com/events/480091672379391/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Direction of C# as a High-Performance Language", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNl4JBa6Fy6GWmNfg"] = CommonStrings.CollateralPowerpoint
                    })
                }),
                Location = "Rivne (Ukraine)",
                Date = new DateTime(2017, 2, 18)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Getting started with .Net Core", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNkTLmI5BWv6FYH0A"] = CommonStrings.CollateralPowerpoint,
                        ["https://www.youtube.com/watch?v=FH3dMIVCDVY"] = CommonStrings.CollateralVideoRus
                    })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2017, 2, 8)
            }
        };

        private static EventModel[] Events2016 { get; } = {
            new EventModel
            {
                Title = "Most JS Frameworks Day 2016",
                Link = "http://frameworksdays.com/event/most-js-fwdays-2016/review/lets-build-a-web-application",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Let's Build a Web Application (and Talk About Ways to Improve Bad Parts)", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNgQofEVXN-RC-1fw"] = CommonStrings.CollateralPowerpoint,
                        ["https://www.youtube.com/watch?v=teQkX9GoV1g"] = CommonStrings.CollateralVideoRus
                    })
                }),
                Location = "Kyiv (Ukraine)",
                Date = new DateTime(2016, 12, 4)
            },
            new EventModel
            {
                Title = "XP Days Ukraine",
                Link = "http://xpdays.com.ua/speaker/igor-fesenko",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Continuous Learning using Application Performance Management & Monitoring",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNfNE_1PZAWuz27DA"] = CommonStrings.CollateralPowerpoint,
                            ["https://www.youtube.com/watch?v=1OZNgD_eIqU"] = CommonStrings.CollateralVideoRus
                        })
                }),
                Location = "Kyiv (Ukraine)",
                Date = new DateTime(2016, 11, 11)
            },
            new EventModel
            {
                Title = "Metrics Morning@Lohika",
                Link = "http://morning.lohika.com/past-events/metrics-morning",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Continuous Learning using Application Performance Management & Monitoring",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNbe6H8GU6_9ebEew"] = CommonStrings.CollateralPowerpoint,
                            ["https://www.youtube.com/watch?v=aFsk7yDkwBs"] = CommonStrings.CollateralVideoRus
                        })
                }),
                Location = "Lviv (Ukraine)",
                Date = new DateTime(2016, 11, 5)
            },
            new EventModel
            {
                Title = ".NET Framework Day 2016",
                Link = "http://frameworksdays.com/event/net-framework-day-2016/review/direction-of-csharp",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Direction of C# as a High-Performance Language", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNakepnrf471XgHIQ"] = CommonStrings.CollateralPowerpoint,
                        ["https://www.youtube.com/watch?v=rf6tZVog6LE"] = CommonStrings.CollateralVideoRus
                    })
                }),
                Location = "Kyiv (Ukraine)",
                Date = new DateTime(2016, 10, 22)
            },
            new EventModel
            {
                Title = "IT Weekend Ukraine 2016",
                Link = "https://ukraine.itweekend.ua/en/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Direction of C# as a High-Performance Language", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNX5JautJLHYTLU0w"] = CommonStrings.CollateralPowerpoint
                    })
                }),
                Location = "Kyiv (Ukraine)",
                Date = new DateTime(2016, 9, 17)
            },
            new EventModel
            {
                Title = "AzureDay",
                Link = "http://azureday.net/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Workshop: Application Insights From The Ground Up", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNW8LO4YXCtdtYXIg"] = CommonStrings.CollateralPowerpoint,
                        ["https://github.com/Ky7m/DemoCode/tree/master/ApplicationInsightsWorkshop"] = CommonStrings.CollateralDemoCode
                    }),
                    new EventModelItem("Enabling DevTest in Microsoft Azure", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNVjIxVov3ZsewJ9Q"] = CommonStrings.CollateralPowerpoint
                    }),
                    new EventModelItem("Channel 9: DevOps Video Lessons", new ImmutableDictionaryBuilder<string, string>
                    {
                        ["https://channel9.msdn.com/Shows/DevOpsUA/Enabling-DevTest-in-Azure"] = CommonStrings.CollateralVideoRus,
                        ["https://channel9.msdn.com/Shows/DevOpsUA/Enabling-DevTest-in-Azure-demo"] = CommonStrings.CollateralVideoRus
                    })
                }),
                Location = "Kyiv (Ukraine)",
                Date = new DateTime(2016, 6, 18)
            },
            new EventModel
            {
                Title = "Application Innovation Strategy Briefing",
                Link = "https://www.microsoftevents.com/profile/form/index.cfm?PKformID=0x138568b7ed",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem(
                        "The Application Innovation Strategy Briefing is a mix of discussion, application architecture design, white boarding and hands-on labs. Your team will design the future of business with new functionalities that augment existing business applications and drive development and execution of new initiatives.")
                }),
                Location = "Cambridge, MA (USA)",
                Date = new DateTime(2016, 6, 9)
            },
            new EventModel
            {
                Title = "Modernize your business - a Modern Requirements and Microsoft Showcase",
                Link = "https://www.microsoftevents.com/profile/form/index.cfm?PKformID=0x148353d22c",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("OPTIMIZE your business by streamlining application lifecycle management process")
                }),
                Location = "San Francisco (USA)",
                Date = new DateTime(2016, 4, 28)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem(".NET Framework 461 & C# 67",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=auSzzmaUTWU"] = CommonStrings.CollateralVideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNSDzCr6-mUwqWbmg"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2016, 4, 20)
            },
            new EventModel
            {
                Title = "Ciklum Lviv .Net Saturday",
                Link = "http://dou.ua/calendar/10488/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("The Present and Future of C# or I Know What You Did on Your Last Project",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNRKE6T2hmJ6C5FVA"] = CommonStrings.CollateralPowerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/CiklumLvivDotNetSaturday"] = CommonStrings.CollateralDemoCode
                        })
                }),
                Location = "Lviv (Ukraine)",
                Date = new DateTime(2016, 4, 16)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Exception Handling - Advanced Tips & Tricks",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=_Gw_9KeJlbg"] = CommonStrings.CollateralVideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNQ45EPNQN2UITD7g"] = CommonStrings.CollateralPowerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/ExceptionHandlingAdvancedTipsAndTricks"] = CommonStrings.CollateralDemoCode
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2016, 2, 24)
            },
            new EventModel
            {
                Title = "Web UI Community - SoftServe",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Web Development in Future",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=Om_NPxSuEf8"] = CommonStrings.CollateralVideoRus,
                            ["https://sway.com/LxEYWidCXvki-OHN"] = CommonStrings.CollateralSway
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2016, 2, 18)
            },
            new EventModel
            {
                Title = ".Net Morning@Lohika",
                Link = "http://morning.lohika.com/past-events/net-morninglohika",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Effective Memory Management: Memory Hygiene",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=vUX2wFciHrs"] = CommonStrings.CollateralVideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNFob8ClLkvBNX-fQ"] = CommonStrings.CollateralPowerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/EffectiveMemoryManagement"] = CommonStrings.CollateralDemoCode
                        })
                }),
                Location = "Lviv (Ukraine)",
                Date = new DateTime(2016, 2, 13)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("How C# 6.0 Simplifies Your Code",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=gUfrK1rGWB0"] = CommonStrings.CollateralVideoRus
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2016, 2, 10)
            },
            new EventModel
            {
                Title = "Enable Application Innovation Immersion",
                Link = "https://www.microsoftevents.com/profile/form/index.cfm?PKformID=0x40756dbc3",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem(
                        "The Enable Application Innovation Immersion is a mix of discussion, application architecture design, white boarding and hands-on labs. Your team will design the future of business with new functionalities that augment existing business applications and drive development and execution of new initiatives.")
                }),
                Location = "Dallas (USA)",
                Date = new DateTime(2016, 1, 26)
            }
        };

        private static EventModel[] Events2015 { get; } = {
            new EventModel
            {
                Title = "Tech#Skills_Day 1.2.",
                Link = "https://www.facebook.com/events/1650103461936579/1654170548196537/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Effective Memory Management: Memory Hygiene",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNFob8ClLkvBNX-fQ"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = "Lviv (Ukraine)",
                Date = new DateTime(2015, 12, 21)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("C# GTD : Get Things Done in Bloody Enterprise",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=hOjNpHeMvvw"] = CommonStrings.CollateralVideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNLl8a5HhMnyufFxA"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2015, 12, 16)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("C# : Hack yourself",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=WHwy0vDd8Tg"] = CommonStrings.CollateralVideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNKIdej9rMPxG9coQ"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2015, 11, 19)
            },
            new EventModel
            {
                Title = "IT Weekend Kharkiv",
                Link = "https://itweekend.ua/en/announcements/itw-kh-nov/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Web Development in Future",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://sway.com/LxEYWidCXvki-OHN"] = CommonStrings.CollateralSway
                        })
                }),
                Location = "Kharkiv (Ukraine)",
                Date = new DateTime(2015, 11, 14)
            },
            new EventModel
            {
                Title = "IT Weekend Ivano-Frankivsk",
                Link = "https://itweekend.ua/en/announcements/itw-if-nov/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Effective Memory Management: Memory Hygiene",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNJH_-PWUG3h0sM6g"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = "Ivano-Frankivsk (Ukraine)",
                Date = new DateTime(2015, 11, 7)
            },
            new EventModel
            {
                Title = "IT Weekend Rivne II",
                Link = "https://itweekend.ua/en/announcements/itw-rv-oct/",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Web Development in Future",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://sway.com/LxEYWidCXvki-OHN"] = CommonStrings.CollateralSway
                        })
                }),
                Location = "Rivne (Ukraine)",
                Date = new DateTime(2015, 10, 24)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Findings from .NET Microsoft Open Source Projects",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=m9MFhqq0g3k"] = CommonStrings.CollateralVideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNGmoPgxIKJYEsNcg"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2015, 10, 21)
            },
            new EventModel
            {
                Title = "Pacemaker: .NET - SoftServe",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Effective Memory Management: Memory Hygiene",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=jpOgZBGL66g"] = CommonStrings.CollateralVideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNFob8ClLkvBNX-fQ"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = "Chernivtsi (Ukraine)",
                Date = new DateTime(2015, 10, 17)
            },
            new EventModel
            {
                Title = "Pacemaker: WebUI - SoftServe",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Web Development in Future",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://sway.com/LxEYWidCXvki-OHN"] = CommonStrings.CollateralSway
                        })
                }),
                Location = "Kyiv (Ukraine)",
                Date = new DateTime(2015, 9, 5)
            },
            new EventModel
            {
                Title = "Web UI Community - SoftServe",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("TypeScript",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=UqustygnUgk"] = CommonStrings.CollateralVideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNCN_UOi3o5qOKQjw"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2015, 7, 24)
            },
            new EventModel
            {
                Title = "Development Process Community - SoftServe",
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Git Workflows For Enterprise Teams",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=RlJOX5bbh1U"] = CommonStrings.CollateralVideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNBvUxr7cJJmWnSIA"] = CommonStrings.CollateralPowerpoint
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2015, 6, 2)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftserve,
                Items = ImmutableArray.CreateRange(new[]
                {
                    new EventModelItem("Making .Net Applications Faster",
                        new ImmutableDictionaryBuilder<string, string>
                        {
                            ["https://www.youtube.com/watch?v=Rgvr1hynOmE"] = CommonStrings.CollateralVideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuM-syDv8BchBHdKIw"] = CommonStrings.CollateralPowerpoint,
                            ["https://github.com/Ky7m/MakingDotNETApplicationsFaster"] =
                            CommonStrings.CollateralDemoCode
                        })
                }),
                Location = CommonStrings.LocationWebcast,
                Date = new DateTime(2015, 2, 11)
            }
        };
    }
}
