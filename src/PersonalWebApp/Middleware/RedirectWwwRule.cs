using System;
using System.Net;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;

namespace PersonalWebApp.Middleware
{
    public class RedirectWwwRule : IRule
    {
        private int StatusCode { get; } = (int)HttpStatusCode.MovedPermanently;

        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;
            var requestHost = request.Host;

            if (requestHost.Host.StartsWith("www", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(requestHost.Host, "localhost", StringComparison.OrdinalIgnoreCase) ||
                requestHost.Host.IndexOf("ifesenko.com", StringComparison.OrdinalIgnoreCase) < 0)
            {
                context.Result = RuleResult.ContinueRules;
                return;
            }

            var newPath = request.Scheme + "://www." + requestHost.Value + request.PathBase + request.Path + request.QueryString;

            var response = context.HttpContext.Response;
            response.StatusCode = StatusCode;
            response.Headers[HeaderNames.Location] = newPath;
            context.Result = RuleResult.EndResponse; // Do not continue processing the request        
        }
    }
}