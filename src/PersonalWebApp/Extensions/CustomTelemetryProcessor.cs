using System;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace PersonalWebApp.Extensions
{
    [UsedImplicitly]
    public sealed class CustomTelemetryProcessor : ITelemetryProcessor
    {
        private readonly ITelemetryProcessor _next;

        public CustomTelemetryProcessor(ITelemetryProcessor next) => _next = next;

        public void Process(ITelemetry item)
        {
            if (item.Context.Operation.Name != null)
            {
                if (item.Context.Operation.Name.Equals("GET /hc", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }

            // Filter out synthetic requests
            if (!string.IsNullOrEmpty(item.Context.Operation.SyntheticSource))
            {
                return;
            }

            _next.Process(item);
        }
    }
}
