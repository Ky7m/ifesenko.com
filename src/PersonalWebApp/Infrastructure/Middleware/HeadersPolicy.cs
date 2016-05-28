using System.Collections.Generic;

namespace PersonalWebApp.Infrastructure.Middleware
{
    public sealed class HeadersPolicy
    {
        public ISet<string> RemoveHeaders { get; } = new HashSet<string>();
    }
}