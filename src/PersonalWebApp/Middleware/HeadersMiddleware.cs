using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PersonalWebApp.Middleware
{
    public sealed class HeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HeadersPolicy _policy;

        public HeadersMiddleware(RequestDelegate next, HeadersPolicy policy)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (next == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }

            _next = next;
            _policy = policy;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.Response;

            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            var headers = response.Headers;

            foreach (var header in _policy.RemoveHeaders)
            {
                headers.Remove(header);
            }

            await _next(context);
        }

    }
}
