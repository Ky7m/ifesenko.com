using System;

namespace PersonalWebApp.Models;

public sealed class HomeModel
{
    public HomeModel() => Events = [];
    public EventModel[] Events { get; set; }
    public bool IsItAllEvents { get; set; }
}