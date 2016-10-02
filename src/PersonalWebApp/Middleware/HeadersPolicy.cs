using System.Collections.Generic;

namespace PersonalWebApp.Middleware
{
    public sealed class HeadersPolicy
    {
        public ISet<string> RemoveHeaders { get; } = new HashSet<string>();
    }
}