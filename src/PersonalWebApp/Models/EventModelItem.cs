using System.Collections.Generic;
using System.Collections.Immutable;

namespace PersonalWebApp.Models
{
    public sealed class EventModelItem
    {
        public EventModelItem(string description, ImmutableDictionary<string, string> collateral)
        {
            Collateral = collateral;
            Description = description;
        }

        public EventModelItem(string description, Dictionary<string, string> collateral) : this(description, collateral.ToImmutableDictionary())
        {
        }

        public EventModelItem(string description) : this(description, ImmutableDictionary<string, string>.Empty)
        {
        }

        public string Description { get; }
        public ImmutableDictionary<string, string> Collateral { get; }
    }
}