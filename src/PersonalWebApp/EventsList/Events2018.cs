using System;
using System.Collections.Generic;
using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList
{
    internal static class Events2018
    {
        public static EventModel[] List { get; } =
        {
            new EventModel
            {
                Title = "iForum 2018",
                Link = "https://2018.iforum.ua/en/speakers/igor-fesenko/",
                Items = new[]
                {
                    new EventModelItem("To Be Announced",
                        new Dictionary<string, string>
                        {
//                            ["https://1drv.ms/p/s!AmdJq1kgIxHUj5AYrB6EOv9kuN5ACw"] = CommonStrings.CollateralPowerpoint,
//                            ["https://www.youtube.com/watch?v=erDG526EWH4"] = CommonStrings.CollateralVideoRus
                        })
                },
                Location = CommonStrings.LocationKyiv,
                Date = new DateTime(2018, 4, 25)
            },
            new EventModel
            {
                Title = ".NET fwdays '18",
                Link = "https://frameworksdays.com/event/dotnet-fwdays-2018",
                Items = new[]
                {
                    new EventModelItem("To Be Announced",
                        new Dictionary<string, string>
                        {
//                            ["https://1drv.ms/p/s!AmdJq1kgIxHUj5AYrB6EOv9kuN5ACw"] = CommonStrings.CollateralPowerpoint,
//                            ["https://www.youtube.com/watch?v=erDG526EWH4"] = CommonStrings.CollateralVideoRus
                        })
                },
                Location = CommonStrings.LocationKyiv,
                Date = new DateTime(2018, 4, 15)
            }
        };
    }
}