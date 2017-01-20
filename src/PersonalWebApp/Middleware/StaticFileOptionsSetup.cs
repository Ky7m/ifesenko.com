using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace PersonalWebApp.Middleware
{
    public class StaticFileOptionsSetup : IConfigureOptions<StaticFileOptions>
    {
        private readonly CachedWebRootFileProvider _cachedWebRoot;

        public StaticFileOptionsSetup(CachedWebRootFileProvider cachedWebRoot)
        {
            _cachedWebRoot = cachedWebRoot;
        }

        public void Configure(StaticFileOptions options)
        {
            options.FileProvider = _cachedWebRoot;
        }
    }
}