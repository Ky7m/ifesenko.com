using System;
using System.Collections.Generic;
using PersonalWebApp.Extensions;
using PersonalWebApp.Models;

namespace PersonalWebApp.EventsList
{
    internal static class Events2021
    {
        public static EventModel[] List { get; } =
        {
            new EventModel
            {
                Title = "AWS User Group: .NET Meetup",
                Link = "https://net.aws-user-group.com.ua",
                Items = new[]
                {
                    new EventModelItem("Architecting Cloud Native .NET Applications",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkOFjIjrN8iNKOz2HRA"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Online,
                Date = new DateTime(2021, 5, 27)
            },
            new EventModel
            {
                Title = "Cloud Builders Conf: .NET & Java",
                Link = "https://www.facebook.com/events/861746014401408",
                Location = CommonStrings.Online,
                Date = new DateTime(2021, 3, 25)
            }
        };
    }
}