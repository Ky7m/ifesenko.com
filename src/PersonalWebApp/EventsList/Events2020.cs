using System;
using System.Collections.Generic;
using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList
{
    internal static class Events2020
    {
        public static EventModel[] List { get; } =
        {
            new EventModel
            {
                Title = ".NET fwdays'20",
                Link = "https://fwdays.com/event/dotnet-fwdays-2020",
                Items = new[]
                {
                    new EventModelItem("C# 8: Be Good, Get Good or Give Up",
                        new Dictionary<string, string>())
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2020, 4, 11)
            },
            new EventModel
            {
                Title = "Svitla Smart Talk: .NET. Lifting Your Code Quality Higher",
                Link = "https://www.facebook.com/events/191429968902776/",
                Items = new[]
                {
                    new EventModelItem("Lifting Your C# Code Quality Higher",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkMVbbthLSqDt6fih9w?e=8cNVFf"] = CommonStrings.Powerpoint,
                            //["https://github.com/Ky7m/DemoCode/tree/master/HolidayPartySF2018"] = CommonStrings.DemoCode
                        })
                },
                Location = CommonStrings.Kyiv,
                Date = new DateTime(2020, 2, 27)
            },
        };
    }
}