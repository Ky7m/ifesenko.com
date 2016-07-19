using System;
using Microsoft.AspNetCore.Builder;

namespace PersonalWebApp.Infrastructure.Middleware
{
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseHeadersMiddleware(this IApplicationBuilder app, HeadersBuilder builder)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        return app.UseMiddleware<HeadersMiddleware>(builder.Build());
    }
}
}