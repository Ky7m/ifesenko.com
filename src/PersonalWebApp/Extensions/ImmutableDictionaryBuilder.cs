using System.Collections.Immutable;

namespace PersonalWebApp.Extensions
{
    public class ImmutableDictionaryBuilder<TKey, TValue>
    {
        private readonly ImmutableDictionary<TKey, TValue>.Builder _builder;

        public ImmutableDictionaryBuilder() => _builder = ImmutableDictionary.CreateBuilder<TKey, TValue>();

        public TValue this[TKey key]
        {
            set => _builder[key] = value;
        }

        public ImmutableDictionary<TKey, TValue> ToImmutable() => _builder.ToImmutable();
    }
}
