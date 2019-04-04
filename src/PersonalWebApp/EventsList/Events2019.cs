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
                Title = "DevOps Fest 2019",
                Link = "http://www.devopsfest.com.ua/",
                Items = new[]
                {
                    new EventModelItem("DevOps: Be good, Get good or Give up",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkKxH4D42htegWfTxAw"] = CommonStrings.CollateralPowerpoint
                        })
                },
                Location = CommonStrings.LocationKyiv,
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
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkKxGXsD8hZfqq2NlLA"] = CommonStrings.CollateralPowerpoint
                        })
                },
                Location = CommonStrings.LocationKyiv,
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
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkKcUlf0Se_5cmaJLtA"] = CommonStrings.CollateralPowerpoint
                        })
                },
                Location = "Washington, D.C. (USA)",
                Date = new DateTime(2019, 2, 4)
            }
        };
    }
}