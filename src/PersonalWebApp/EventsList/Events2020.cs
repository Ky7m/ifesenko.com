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
        };
    }
}