using System;
using System.Collections.Immutable;

namespace PersonalWebApp.Models
{
    public sealed class EventModel
    {
        public EventModel() => Items = ImmutableArray<EventModelItem>.Empty;

        public string Title { get; set; }
        public string Link { get; set; }
        public ImmutableArray<EventModelItem> Items { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
    }
}