using System.Collections.Immutable;
using PersonalWebApp.Extensions;

namespace PersonalWebApp.Services.Implementation.CloudStorageService.Model
{
    public sealed class EventModelItem
    {
        public EventModelItem(string description, ImmutableDictionary<string, string> collateral)
        {
            Collateral = collateral;
            Description = description;
        }

        public EventModelItem(string description, ImmutableDictionaryBuilder<string, string> collateral) : this(description, collateral.ToImmutable())
        {
        }

        public EventModelItem(string description) : this(description, ImmutableDictionary<string, string>.Empty)
        {
        }

        public string Description { get; }
        public ImmutableDictionary<string, string> Collateral { get; }
    }
}