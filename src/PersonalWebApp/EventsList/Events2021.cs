using System;
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
                Title = "Cloud Builders Conf: .NET & Java",
                Link = "https://www.facebook.com/events/861746014401408",
                Location = CommonStrings.Webcast,
                Date = new DateTime(2021, 3, 25)
            }
        };
    }
}