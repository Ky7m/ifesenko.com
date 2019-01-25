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
                Title = "Microsoft Ignite | The Tour Government",
                Link = "https://www.microsoft.com/en-us/ignite-the-tour/washington-dc",
//                Items = new[]
//                {
////                    new EventModelItem("What's new in .NET Core 2.2 & ASP.NET Core 2.2//// - Top Features",
////                        new Dictionary<string, string>
////                        {
////                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkJkNG37NqzSRqt4LxQ"] = CommonStrings.CollateralPowerpoint,
////                            ["https://github.com/Ky7m/DemoCode/tree/master/HolidayPartySF2018"] =
////                                CommonStrings.CollateralDemoCode
////                        })
//                },
                Location = "Washington, D.C. (USA)",
                Date = new DateTime(2019, 2, 4)
            }
        };
    }
}