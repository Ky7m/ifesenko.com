using System;

namespace PersonalHomePage.Models
{
    public sealed class HomeModel
    {
        public HomeModel()
        {
            Events = Array.Empty<EventModel>();
        }
        public StatsModel Stats { get; set; }
        public EventModel[] Events { get; set; }
    }
}