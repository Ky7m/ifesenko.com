using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace PersonalHomePage.Extensions
{
    public class ApplicationInsightsTelemetryInitializer : ITelemetryInitializer
    {

        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Component.Version = BundleConfig.Version;
        }
    }
}