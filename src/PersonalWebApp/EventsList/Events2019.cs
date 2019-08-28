using System;
using System.Collections.Generic;
using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList
{
    internal static class Events2019
    {
        public static EventModel[] List { get; } =
        {
            new EventModel
            {
                Title = "ITEAHub Workshop: Designing and Building a Chatbot",
                Link = "https://www.facebook.com/events/225823375004502/",
                Items = new[]
                {
                    new EventModelItem("Workshop: Designing and Building a Chatbot",
                        new Dictionary<string, string>())
                },
                Location = CommonStrings.Lviv,
                Date = new DateTime(2019, 9, 9)
            },
            new EventModel
            {
                Title = "IТ Weekend Ukraine 2019",
                Link = "https://itweekend.events/event/it-weekend-ukraine-2019-ukrainian-it-awards-2019/",
                Items = new[]
                {
                    new EventModelItem("Lessons From The Trenches: Designing and Building a Chatbot",
                        new Dictionary<string, string>()),
                    new EventModelItem("What I Learned Making an Integration Between Global Ecosystems",
                        new Dictionary<string, string>())
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2019, 9, 7)
            },
            new EventModel
            {
                Title = "AllStars-IT Ukraine: Data & Logging Tech Meetup",
                Link = "https://www.facebook.com/events/741738502913197/",
                Items = new[]
                {
                    new EventModelItem("A Practical Look at Logging, Metrics, and Events in ASP.NET Core",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkLJP3A8ziSl6Ul6ovQ?e=TbBU6i"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/HolidayPartySF2018"] = CommonStrings.DemoCode
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2019, 9, 5)
            },
            new EventModel
            {
                Title = "Meet-Up .Net. Lviv",
                Link = "https://www.facebook.com/events/2367809860143711/",
                Items = new[]
                {
                    new EventModelItem("A Practical Look at Logging, Metrics, and Events in ASP.NET Core",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkLJP3A8ziSl6Ul6ovQ?e=TbBU6i"] = CommonStrings.Powerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/HolidayPartySF2018"] = CommonStrings.DemoCode
                        })
                },
                Location = CommonStrings.Lviv,
                Date = new DateTime(2019, 8, 28)
            },
            new EventModel
            {
                Title = "Digital Transformation Conference 2019",
                Link = "https://dtconf.com",
                Items = new[]
                {
                    new EventModelItem("Lessons From The Trenches: Designing and Building a Chatbot",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkLENHu1rquc3YjiE3w?e=AoBrKm"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2019, 6, 22)
            },
            new EventModel
            {
                Title = "DevOps Fest 2019",
                Link = "http://www.devopsfest.com.ua/",
                Items = new[]
                {
                    new EventModelItem("DevOps: Be good, Get good or Give up",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkKxH4D42htegWfTxAw"] = CommonStrings.Powerpoint,
                            ["https://www.youtube.com/watch?v=YXbkgU0tik8"] = CommonStrings.VideoRus
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2019, 4, 6)
            },
            new EventModel
            {
                Title = "Tech Meetup: “Securing your web applications with ASP.NET“",
                Link = "https://www.facebook.com/events/2217721638293124/",
                Items = new[]
                {
                    new EventModelItem("Securing Your Web Applications With ASP.NET",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkKxGXsD8hZfqq2NlLA"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2019, 4, 5)
            },
            new EventModel
            {
                Title = "Microsoft Ignite | The Tour Government",
                Link = "https://www.microsoft.com/en-us/ignite-the-tour/washington-dc",
                Items = new[]
                {
                    new EventModelItem("Scale Up An App And Engineering Processes To The Next Level With Azure​",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkKcUlf0Se_5cmaJLtA"] = CommonStrings.Powerpoint
                        })
                },
                Location = "Washington, D.C. (USA)",
                Date = new DateTime(2019, 2, 4)
            }
        };
    }
}