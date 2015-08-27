using Microsoft.ApplicationInsights.Extensibility;

namespace PersonalHomePage.Extensions
{
    public class ApplicationInsightsConfigInitializer
         : IContextInitializer
    {

        public void Initialize(Microsoft.ApplicationInsights.DataContracts.TelemetryContext context)
        {
            // Configure app version
            context.Component.Version = BundleConfig.Version;
        }
    }
}