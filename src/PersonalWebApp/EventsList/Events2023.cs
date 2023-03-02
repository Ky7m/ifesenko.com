using System;
using System.Collections.Generic;
using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList;

internal static class Events2023
{
    public static EventModel[] List { get; } = {
        new()
        {
            Title = "DevOps fwdays'23 conference",
            Link = "https://fwdays.com/en/event/devops-fwdays-2023/review/modern-devops-real-life-applications",
            Items = new[]
            {
                new EventModelItem("Modern DevOps & Real Life Applications. 3.0.0-devops+20230318",
                    new Dictionary<string, string>
                    {
                        ["https://1drv.ms/p/s!AmdJq1kgIxHUkPxHesyZdfXmSQZLcg?e=JZsyD3"] = CommonStrings.Powerpoint
                        // ["https://www.youtube.com/watch?v=kjpbGRhWy50"] = CommonStrings.Recording
                    })
            },
            Location = CommonStrings.Online,
            Date = new DateTime(2023, 3, 18)
        }
    };
}