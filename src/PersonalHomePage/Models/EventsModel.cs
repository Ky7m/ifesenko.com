using PersonalHomePage.Services.Implementation.CloudStorageService.Model;

namespace PersonalHomePage.Models
{
    public sealed class EventsModel
    {
        public EventTableEntity[] Upcoming { get; set; }
        public EventTableEntity[] Previous { get; set; }
    }
}