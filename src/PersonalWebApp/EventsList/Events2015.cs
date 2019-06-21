using System;
using System.Collections.Generic;
using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList
{
    internal static class Events2015
    {
        public static EventModel[] List { get; } =
        {
            new EventModel
            {
                Title = "Tech#Skills_Day 1.2.",
                Link = "https://www.facebook.com/events/1650103461936579/1654170548196537/",
                Items = new[]
                {
                    new EventModelItem("Effective Memory Management: Memory Hygiene",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNFob8ClLkvBNX-fQ"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Lviv,
                Date = new DateTime(2015, 12, 21)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftServe,
                Items = new[]
                {
                    new EventModelItem("C# GTD : Get Things Done in Bloody Enterprise",
                        new Dictionary<string, string>
                        {
                            ["https://www.youtube.com/watch?v=hOjNpHeMvvw"] = CommonStrings.VideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNLl8a5HhMnyufFxA"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Webcast,
                Date = new DateTime(2015, 12, 16)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftServe,
                Items = new[]
                {
                    new EventModelItem("C# : Hack yourself",
                        new Dictionary<string, string>
                        {
                            ["https://www.youtube.com/watch?v=WHwy0vDd8Tg"] = CommonStrings.VideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNKIdej9rMPxG9coQ"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Webcast,
                Date = new DateTime(2015, 11, 19)
            },
            new EventModel
            {
                Title = "IT Weekend Kharkiv",
                Link = "https://itweekend.ua/en/announcements/itw-kh-nov/",
                Items = new[]
                {
                    new EventModelItem("Web Development in Future",
                        new Dictionary<string, string>
                        {
                            ["https://sway.com/LxEYWidCXvki-OHN"] = CommonStrings.Sway
                        })
                },
                Location = CommonStrings.Kharkiv,
                Date = new DateTime(2015, 11, 14)
            },
            new EventModel
            {
                Title = "IT Weekend Ivano-Frankivsk",
                Link = "https://itweekend.ua/en/announcements/itw-if-nov/",
                Items = new[]
                {
                    new EventModelItem("Effective Memory Management: Memory Hygiene",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNJH_-PWUG3h0sM6g"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.IvanoFrankivsk,
                Date = new DateTime(2015, 11, 7)
            },
            new EventModel
            {
                Title = "IT Weekend Rivne II",
                Link = "https://itweekend.ua/en/announcements/itw-rv-oct/",
                Items = new[]
                {
                    new EventModelItem("Web Development in Future",
                        new Dictionary<string, string>
                        {
                            ["https://sway.com/LxEYWidCXvki-OHN"] = CommonStrings.Sway
                        })
                },
                Location = CommonStrings.Rivne,
                Date = new DateTime(2015, 10, 24)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftServe,
                Items = new[]
                {
                    new EventModelItem("Findings from .NET Microsoft Open Source Projects",
                        new Dictionary<string, string>
                        {
                            ["https://www.youtube.com/watch?v=m9MFhqq0g3k"] = CommonStrings.VideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNGmoPgxIKJYEsNcg"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Webcast,
                Date = new DateTime(2015, 10, 21)
            },
            new EventModel
            {
                Title = "Pacemaker: .NET - SoftServe",
                Items = new[]
                {
                    new EventModelItem("Effective Memory Management: Memory Hygiene",
                        new Dictionary<string, string>
                        {
                            ["https://www.youtube.com/watch?v=jpOgZBGL66g"] = CommonStrings.VideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNFob8ClLkvBNX-fQ"] = CommonStrings.Powerpoint
                        })
                },
                Location = "Chernivtsi (Ukraine)",
                Date = new DateTime(2015, 10, 17)
            },
            new EventModel
            {
                Title = "Pacemaker: WebUI - SoftServe",
                Items = new[]
                {
                    new EventModelItem("Web Development in Future",
                        new Dictionary<string, string>
                        {
                            ["https://sway.com/LxEYWidCXvki-OHN"] = CommonStrings.Sway
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2015, 9, 5)
            },
            new EventModel
            {
                Title = "Web UI Community - SoftServe",
                Items = new[]
                {
                    new EventModelItem("TypeScript",
                        new Dictionary<string, string>
                        {
                            ["https://www.youtube.com/watch?v=UqustygnUgk"] = CommonStrings.VideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNCN_UOi3o5qOKQjw"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Webcast,
                Date = new DateTime(2015, 7, 24)
            },
            new EventModel
            {
                Title = "Development Process Community - SoftServe",
                Items = new[]
                {
                    new EventModelItem("Git Workflows For Enterprise Teams",
                        new Dictionary<string, string>
                        {
                            ["https://www.youtube.com/watch?v=RlJOX5bbh1U"] = CommonStrings.VideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuNBvUxr7cJJmWnSIA"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Webcast,
                Date = new DateTime(2015, 6, 2)
            },
            new EventModel
            {
                Title = CommonStrings.DotNetCommunitySoftServe,
                Items = new[]
                {
                    new EventModelItem("Making .Net Applications Faster",
                        new Dictionary<string, string>
                        {
                            ["https://www.youtube.com/watch?v=Rgvr1hynOmE"] = CommonStrings.VideoRus,
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUjuM-syDv8BchBHdKIw"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/MakingDotNETApplicationsFaster"] =
                            CommonStrings.DemoCode
                        })
                },
                Location = CommonStrings.Webcast,
                Date = new DateTime(2015, 2, 11)
            }
        };
    }
}