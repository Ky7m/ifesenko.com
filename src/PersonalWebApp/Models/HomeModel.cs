using System;
using PersonalWebApp.Services.Implementation.CloudStorageService.Model;

namespace PersonalWebApp.Models
{
    public sealed class HomeModel
    {
        public HomeModel()
        {
            Events = Array.Empty<EventModel>();
        }
        public EventModel[] Events { get; set; }
    }
}