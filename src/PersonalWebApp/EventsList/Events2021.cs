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
                Title = "Lviv .NET #30: Public architect interview",
                Link = "https://www.facebook.com/events/2866930863621382/",
                Location = CommonStrings.Online,
                Date = new DateTime(2021, 6, 25)
            },
            new EventModel
            {
                Title = "Svitla Smart Talk: SignalR. Advanced scenarios.",
                Link = "https://www.facebook.com/events/1139786046526133/",
                Items = new[]
                {
                    new EventModelItem("SignalR. Advanced scenarios",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkOI6_F2lIX6kGOhxpw?e=7xxs0z"] = CommonStrings.Powerpoint
                        })
                },
                Location = CommonStrings.Online,
                Date = new DateTime(2021, 6, 23)
            },
            new EventModel
            {
                Title = ".NET Tech Battle",
                Link = "https://www.facebook.com/events/516170829522127/",
                Location = CommonStrings.Online,
                Date = new DateTime(2021, 6, 17)
            },
            new EventModel
            {
                Title = "AWS User Group: .NET Meetup",
                Link = "https://www.meetup.com/AWS-UserGroup-Ukraine/events/278234526/",
                Items = new[]
                {
                    new EventModelItem("Architecting Cloud Native .NET Applications",
                        new Dictionary<string, string>
                        {
                            ["https://1drv.ms/p/s!AmdJq1kgIxHUkOFjIjrN8iNKOz2HRA"] = CommonStrings.Powerpoint,
                            ["https://www.youtube.com/watch?v=YGtd7WTjW2E"] = CommonStrings.Online
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