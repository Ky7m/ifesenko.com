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
                Title = "ITEAHub TechTalk: easy win with C#",
                Link = "https://www.facebook.com/events/2213455178936354/",
                Items = new[]
                {
                    new EventModelItem("Securing Your Web Applications With ASP.NET",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkJEJ9nMcxsqkiqu0ng"] = CommonStrings.CollateralPowerpoint
                        })
                },
                Location = CommonStrings.LocationLviv,
                Date = new DateTime(2018, 9, 17)
            },
            new EventModel
            {
                Title = "Local .NET Conf - Recap of What's New in .NET!",
                Link = "https://www.facebook.com/events/418182088707602/",
                Items = new[]
                {
                    new EventModelItem("What's New in .NET World?",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkJEZ8Ccyi0_zUChRrA"] = CommonStrings.CollateralPowerpoint
                        }),
                    new EventModelItem("Scale Up An App And Engineering Processes To The Next Level With Azure",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkJEfZ-Rg05o5ZEJjEg"] = CommonStrings.CollateralPowerpoint
                        })
                },
                Location = CommonStrings.LocationLviv,
                Date = new DateTime(2018, 9, 13)
            },
            new EventModel
            {
                Title = "TechCases in Architecture #3",
                Link = "https://www.facebook.com/events/302795240506275/",
                Location = CommonStrings.LocationKyiv,
                Items = new []
                    {
                        new EventModelItem("Scale Up An App And Engineering Processes To The Next Level With Azure",
                            new Dictionary<string,string>
                            {
                                ["https://1drv.ms/p/s!AmdJq1kgIxHUkJEIUeLhTEjQlmSbPA"] = CommonStrings.CollateralPowerpoint
                            })
                    },
                Date = new DateTime(2018, 9, 11)
            },
            new EventModel
            {
                Title = "IT Weekend Ukraine 2018",
                Link = "https://itweekend.events/event/it-weekend-ukraine-2018/",
                Location = CommonStrings.LocationKyiv,
                Date = new DateTime(2018, 9, 8)
            },
            new EventModel
            {
                Title = "LvivJS",
                Link = "https://lvivjs.org.ua/",
                Items = new[]
                {
                    new EventModelItem("What I Learned Making an Integration Between Global Ecosystems",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkJA4sfyBe1Jkr8BV5w"] = CommonStrings.CollateralPowerpoint
                        })
                },
                Location = CommonStrings.LocationLviv,
                Date = new DateTime(2018, 9, 1)
            },
            new EventModel
            {
                Title = "iForum 2018",
                Link = "https://2018.iforum.ua/en/speakers/igor-fesenko/",
                Items = new[]
                {
                    new EventModelItem("What I Learned Making a Global Web App",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkII0hLNCxL0ydrqcMw"] = CommonStrings.CollateralPowerpoint
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