using System.Collections.Immutable;
using PersonalWebApp.Extensions;

namespace PersonalWebApp.Services.Implementation.CloudStorageService.Model
{
    public sealed class EventModelItem
    {
        public string Description { get; }
        public ImmutableDictionary<string, string> Collateral { get; }
        
        public EventModelItem(string description, ImmutableDictionary<string, string> collateral) =>
            (Collateral, Description) = (collateral, description);

        public EventModelItem(string description, ImmutableDictionaryBuilder<string, string> collateral) :
            this(description, collateral.ToImmutable())
        {
        }

        public EventModelItem(string description) :
            this(description, ImmutableDictionary<string, string>.Empty)
        {
        }
    }
}