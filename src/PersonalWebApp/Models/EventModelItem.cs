using System.Collections.Generic;

namespace PersonalWebApp.Models;

public sealed class EventModelItem
{
    public string Description { get; }
    public IReadOnlyDictionary<string, string> Collateral { get; }
        
    private static IReadOnlyDictionary<string,string> EmptyDictionary { get; } = new Dictionary<string,string>(0);
        
    public EventModelItem(string description, IReadOnlyDictionary<string, string> collateral) =>
        (Collateral, Description) = (collateral, description);

 
    public EventModelItem(string description) :
        this(description, EmptyDictionary)
    {
    }
}