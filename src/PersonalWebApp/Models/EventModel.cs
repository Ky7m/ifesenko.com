namespace PersonalWebApp.Models;

public sealed class EventModel
{
    public EventModel() => Items = Array.Empty<EventModelItem>();

    public string Title { get; set; } = string.Empty;
    public string? Link { get; set; }
    public IReadOnlyCollection<EventModelItem> Items { get; set; }
    public string Location { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}