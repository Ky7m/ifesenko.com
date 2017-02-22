using Microsoft.ApplicationInsights.Channel;
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
            if (!string.IsNullOrEmpty(item.Context.Operation.SyntheticSource))
            {
                return;
            }

            this.Next.Process(item);
        }
    }
}
