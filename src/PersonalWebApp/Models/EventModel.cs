using System;
using System.Collections.Generic;

namespace PersonalWebApp.Models;

public sealed class EventModel
{
    public EventModel() => Items = Array.Empty<EventModelItem>();

    public string Title { get; set; }
    public string Link { get; set; }
    public IReadOnlyCollection<EventModelItem> Items { get; set; }
    public string Location { get; set; }
    public DateTime Date { get; set; }
}