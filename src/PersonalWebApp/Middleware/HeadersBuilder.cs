using System;

namespace PersonalWebApp.Middleware
{
    public sealed class HeadersBuilder
    {
        private readonly HeadersPolicy _policy = new HeadersPolicy();

        public HeadersBuilder RemoveHeader(string header)
        {
            if (string.IsNullOrEmpty(header))
            {
                throw new ArgumentNullException(nameof(header));
            }

            _policy.RemoveHeaders.Add(header);
            return this;
        }

        public HeadersPolicy Build()
        {
            return _policy;
        }
    }
}