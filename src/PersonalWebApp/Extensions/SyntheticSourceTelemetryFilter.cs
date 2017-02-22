using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace PersonalWebApp.Extensions
{
    public sealed class SyntheticSourceTelemetryFilter : ITelemetryProcessor
    {
        private ITelemetryProcessor Next { get; }

        public SyntheticSourceTelemetryFilter(ITelemetryProcessor next)
        {
            Next = next;
        }

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

            this.Next.Process(item);
        }
    }
}
