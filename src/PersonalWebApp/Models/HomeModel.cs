namespace PersonalWebApp.Models
{
    public sealed class HomeModel
    {
        public HomeModel()
        {
            Events = new EventModel[0];
        }
        public StatsModel Stats { get; set; }
        public EventModel[] Events { get; set; }
    }
}