using System;
using System.Collections.Generic;
using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList
{
    internal static class Events2017
    {
        public static EventModel[] List { get; } =
        {
            new EventModel
            {
                Title = "Effective Team Work and Power of ASP.NET Core and Node.js",
                Link = "https://www.facebook.com/events/130650640939464",
                Items = new[]
                {
                    new EventModelItem("Take a Power of ASP.NET Core and Node.js Using JavaScript Services",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUj4JWbzxEEfD9p5cnig"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Lviv,
                Date = new DateTime(2017, 12, 26)
            },
            new EventModel
            {
                Title = "IT Talk Kharkiv: Under the Hood of C# and Power of ASP.NET Core & Node.js",
                Link = "https://www.meetup.com/IT-talk-Kharkiv/events/245634133/",
                Items = new[]
                {
                    new EventModelItem("C# Language Internals",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuMA0ujGpZEBr94v0Q"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/CSharpInternals"] = CommonStrings.DemoCode
                        }),
                    new EventModelItem("Take a Power of ASP.NET Core and Node.js Using JavaScript Services",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUj4JWbzxEEfD9p5cnig"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Kharkiv,
                Date = new DateTime(2017, 12, 20)
            },
            new EventModel
            {
                Title = "Cloud Meetup by Microsoft",
                Link = "https://www.facebook.com/events/148131755830888/",
                Items = new[]
                {
                    new EventModelItem(
                        "Applied Microsoft Azure: Cloud Cost Optimization Techniques and Hacks For Enterprise Apps",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUj8grFdvqjEeE2EROLw"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Minsk,
                Date = new DateTime(2017, 12, 12)
            },
            new EventModel
            {
                Title = "Lviv .NET Community Meetup #1 Reload",
                Link = "https://www.facebook.com/events/144988512913185/",
                Items = new[]
                {
                    new EventModelItem("C# Language Internals",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuMA0ujGpZEBr94v0Q"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/CSharpInternals"] = CommonStrings.DemoCode,
                            ["https://www.youtube.com/watch?v=IJNbmivri4U"] = CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Lviv,
                Date = new DateTime(2017, 12, 1)
            },
            new EventModel
            {
                Title = "XP Days Ukraine",
                Link = "http://xpdays.com.ua/speaker/igor-fesenko-2/",
                Items = new[]
                {
                    new EventModelItem("Cloud Cost Optimization Techniques and Hacks For Enterprise Apps",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUj5AYrB6EOv9kuN5ACw"] = CommonStrings.Powerpoint,
                            ["https://www.youtube.com/watch?v=erDG526EWH4"] = CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2017, 11, 10)
            },
            new EventModel
            {
                Title = ".Net Fest 2017",
                Link = "http://dotnetfest.com/",
                Items = new[]
                {
                    new EventModelItem("C# Language Internals",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuMA0ujGpZEBr94v0Q"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/CSharpInternals"] = CommonStrings.DemoCode,
                            ["https://www.youtube.com/watch?v=ImnejSfJVAI"] = CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2017, 10, 28)
            },
            new EventModel
            {
                Title = "Local .NET Conf - Recap of What's New in .NET!",
                Link = "https://www.facebook.com/events/115416022513016/",
                Items = new[]
                {
                    new EventModelItem(".NET Core: Overview and Tools",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUj4JfNKrIZIGkuRzhwQ"] = CommonStrings.Powerpoint
                        }),
                    new EventModelItem("ASP.NET Core in a Nutshell",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUj4Jdub1grMeBWdw9VA"] = CommonStrings.Powerpoint
                        }),
                    new EventModelItem(".NET Standard",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUj4JetthXqYUYCoUvNw"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Lviv,
                Date = new DateTime(2017, 9, 28)
            },
            new EventModel
            {
                Title = "Full Stack Meet Up. Factory edition.",
                Link = "http://factory.daxx.com/",
                Items = new[]
                {
                    new EventModelItem("Take a Power of ASP.NET Core and Node.js Using JavaScript Services",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUj4JWbzxEEfD9p5cnig"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Dnipro,
                Date = new DateTime(2017, 9, 23)
            },
            new EventModel
            {
                Title = "IT talk: .NET Conf Local - Recap of What's New in .NET!",
                Link = "https://www.meetup.com/IT-talk-Dnepr/events/242948073/",
                Items = new[]
                {
                    new EventModelItem(".NET Core: Overview and Tools",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUj4JfNKrIZIGkuRzhwQ"] = CommonStrings.Powerpoint
                        }),
                    new EventModelItem("ASP.NET Core in a Nutshell",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUj4Jdub1grMeBWdw9VA"] = CommonStrings.Powerpoint
                        }),
                    new EventModelItem(".NET Standard",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUj4JetthXqYUYCoUvNw"] = CommonStrings.Powerpoint
                        }),
                    new EventModelItem(".NET Conf Local - Video",
                        new Dictionary<string, string>
                        {
                            ["https://www.youtube.com/watch?v=grwvMmpRDHM"] = CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Dnipro,
                Date = new DateTime(2017, 9, 22)
            },
            new EventModel
            {
                Title = "Microsoft Student Partners Roadshow Lviv",
                Link = "https://2event.com/events/969409/schedule",
                Items = new[]
                {
                    new EventModelItem("My Other Computer is an Azure Data Center",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjv9Zi2NMHyD-tC7gKA"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Lviv,
                Date = new DateTime(2017, 9, 19)
            },
            new EventModel
            {
                Title = "SUBMIT .NET 2017",
                Link = "https://binary-studio.com/submit-net-2017/",
                Items = new[]
                {
                    new EventModelItem("How to Create Reliable Build Tasks and More Using Power of Cake (C# Make)",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjv9XVcZF0-PM0NHrHw"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/CakeBuild"] =
                            CommonStrings.DemoCode
                        })
                },
                Location = CommonStrings.Lviv,
                Date = new DateTime(2017, 9, 16)
            },
            new EventModel
            {
                Title = "AzureDAY 2017",
                Link = "https://azureday.net/Speaker/IFesenko",
                Items = new[]
                {
                    new EventModelItem(
                        "Applied Microsoft Azure: Cloud Cost Optimization Techniques and Hacks For Enterprise Apps",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjv9VA14mgQyBmAZiyQ"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2017, 9, 9)
            },
            new EventModel
            {
                Title = "LvivJS",
                Link = "http://lvivjs.org.ua/",
                Items = new[]
                {
                    new EventModelItem("How to Track Success of Your Application",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjvN0CvlZT3FH9xDD3g"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Lviv,
                Date = new DateTime(2017, 8, 26)
            },
            new EventModel
            {
                Title = "Webinar “What’s New in C# 7.x”",
                Link = "https://dou.ua/calendar/16639/",
                Items = new[]
                {
                    new EventModelItem("What’s New in C# 7.x",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjvNzrpzsPDMh46sePg"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/CSharp7"] = CommonStrings.DemoCode,
                            ["https://www.youtube.com/watch?v=zY0ELCJCcJQ"] = CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Online,
                Date = new DateTime(2017, 7, 27)
            },
            new EventModel
            {
                Title = "IT Weekend Kharkiv: Enterprise Software Solutions",
                Link = "https://itweekend.ua/en/announcements/itw-kh-17m/",
                Items = new[]
                {
                    new EventModelItem("How to Survive in Software Development",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNtEU4vkzhlKSdx3Q"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Kharkiv,
                Date = new DateTime(2017, 5, 27)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftServe,
                Items = new[]
                {
                    new EventModelItem("What’s New in C# 7.0",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNs5VwIZYHzyEqvwQ"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/CSharp7"] = CommonStrings.DemoCode,
                            ["https://www.youtube.com/watch?v=8aCQKnvJWZU"] = CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Online,
                Date = new DateTime(2017, 5, 17)
            },
            new EventModel
            {
                Title = "IT Weekend Rivne",
                Link = "https://itweekend.ua/ua/announcements/itw-rv-17a/",
                Items = new[]
                {
                    new EventModelItem("What’s New in C# 7.0",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNrcFT02PtgE55mxQ"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/CSharp7"] = CommonStrings.DemoCode
                        })
                },
                Location = CommonStrings.Rivne,
                Date = new DateTime(2017, 4, 29)
            },
            new EventModel
            {
                Title = "MeetUp “What’s New in C# 7.0”",
                Link = "https://www.facebook.com/events/1851330288440169/",
                Items = new[]
                {
                    new EventModelItem("What’s New in C# 7.0",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNpSkpb8DlPChyCHA"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/CSharp7"] = CommonStrings.DemoCode
                        })
                },
                Location = CommonStrings.Lviv,
                Date = new DateTime(2017, 4, 26)
            },
            new EventModel
            {
                Title = "MUG Dnipro: C# 7.0 & API",
                Link = "https://www.facebook.com/events/1874373382806547/",
                Items = new[]
                {
                    new EventModelItem("What’s New in C# 7.0",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNpSkpb8DlPChyCHA"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/CSharp7"] = CommonStrings.DemoCode,
                            ["https://www.youtube.com/watch?v=PL6GBQBf9-I&t=4m"] = CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Dnipro,
                Date = new DateTime(2017, 4, 22)
            },

            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftServe,
                Items = new[]
                {
                    new EventModelItem("Cake (C# Make)",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNogknwMLZZSQmfiA"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/CakeBuild"] =
                            CommonStrings.DemoCode,
                            ["https://www.youtube.com/watch?v=1KQOUzY555E"] = CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Online,
                Date = new DateTime(2017, 4, 19)
            },
            new EventModel
            {
                Title = "TalkIT Ivano-Frankivsk #.Net",
                Link = "https://www.facebook.com/events/200151533802892/",
                Items = new[]
                {
                    new EventModelItem("Why You Should Take Another Look at C# in 2017",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNnuN2194rbhhrwOg"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.IvanoFrankivsk,
                Date = new DateTime(2017, 4, 1)
            },
            new EventModel
            {
                Title = ".NET Framework Day 2017",
                Link = "http://frameworksdays.com/event/dotnet-fwdays-2017/review/what-is-new-in-c-sharp-7-0",
                Items = new[]
                {
                    new EventModelItem("What’s New in C# 7.0",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNm65LELY2o3pegng"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/CSharp7"] = CommonStrings.DemoCode,
                            ["https://www.youtube.com/watch?v=n3PkTM32DCY"] = CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2017, 3, 25)
            },
            new EventModel
            {
                Title = "Rivne .NET Community inaugural meeting",
                Link = "https://www.facebook.com/events/480091672379391/",
                Items = new[]
                {
                    new EventModelItem("Direction of C# as a High-Performance Language",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNl4JBa6Fy6GWmNfg"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Rivne,
                Date = new DateTime(2017, 2, 18)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftServe,
                Items = new[]
                {
                    new EventModelItem("Getting started with .Net Core",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNkTLmI5BWv6FYH0A"] = CommonStrings.Powerpoint,
                            ["https://www.youtube.com/watch?v=FH3dMIVCDVY"] = CommonStrings.Recording
                        })
                },
                Location = CommonStrings.Online,
                Date = new DateTime(2017, 2, 8)
            }
        };
    }
}