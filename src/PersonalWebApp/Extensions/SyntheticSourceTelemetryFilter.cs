using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace PersonalWebApp.Extensions
{
    public sealed class SyntheticSourceTelemetryFilter : ITelemetryProcessor
    {
        private readonly ITelemetryProcessor _next;

        public SyntheticSourceTelemetryFilter(ITelemetryProcessor next) => _next = next;

        public void Process(ITelemetry item)
        {
            // Filter out synthetic requests
            if (!string.IsNullOrEmpty(item.Context.Operation.SyntheticSource))
            {
                return;
            }
            // Filter out “fast” requests
            var request = item as RequestTelemetry;
            if (request?.Duration.Milliseconds < 100)
            {
                return;
            }

            _next.Process(item);
        }
    }
}
