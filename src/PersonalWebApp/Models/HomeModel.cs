using System;

namespace PersonalWebApp.Models;

public sealed class HomeModel
{
    public HomeModel() => Events = Array.Empty<EventModel>();
    public EventModel[] Events { get; set; }
    public bool IsItAllEvents { get; set; }
}