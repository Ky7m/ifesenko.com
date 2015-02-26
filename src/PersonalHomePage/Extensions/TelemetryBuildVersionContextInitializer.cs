using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using PersonalHomePage.Controllers;

namespace PersonalHomePage.Extensions
{
    public sealed class TelemetryBuildVersionContextInitializer : IContextInitializer
    {
        public void Initialize(TelemetryContext context)
        {
            context.Properties["BuildVersion"] = System.Reflection.Assembly.GetAssembly(typeof(HomeController)).GetName().Version.ToString();
        }
    }
}