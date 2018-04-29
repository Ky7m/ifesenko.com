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
                    new EventModelItem("What I Learned Making a Global Web App",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkII0hLNCxL0ydrqcMw"] = CommonStrings.CollateralPowerpoint,
//                            ["https://www.youtube.com/watch?v=erDG526EWH4"] = CommonStrings.CollateralVideoRus
                        })
                },
                Location = CommonStrings.LocationKyiv,
                Date = new DateTime(2018, 4, 25)
            },
            new EventModel
            {
                Title = "Global Azure Bootcamp Lviv '18",
                Link = "https://www.facebook.com/events/2038968596390170/",
                Items = new[]
                {
                    new EventModelItem("Create Global Web App with Microsoft Azure",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkIIx_lcJ-B2ID0FpAw"] = CommonStrings.CollateralPowerpoint,
                            ["https://www.youtube.com/watch?v=BkRhK-2bfAk"] = CommonStrings.CollateralVideoRus
                        })
                },
                Location = CommonStrings.LocationLviv,
                Date = new DateTime(2018, 4, 21)
            },
            new EventModel
            {
                Title = ".NET fwdays '18",
                Link = "https://frameworksdays.com/event/dotnet-fwdays-2018",
                Items = new[]
                {
                    new EventModelItem("Asynchronous Processing and Multithreading Are Always Fun",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUj_8sOR6OF_mS5DFRNg"] = CommonStrings.CollateralPowerpoint,
                            ["https://github.com/Ky7m/DemoCode/tree/master/AsynchronousAndMultithreading"] = CommonStrings.CollateralDemoCode,
                            ["https://www.youtube.com/watch?v=TR93nhJgHH0"] = CommonStrings.CollateralVideoRus
                        })
                },
                Location = CommonStrings.LocationKyiv,
                Date = new DateTime(2018, 4, 15)
            }
        };
    }
}