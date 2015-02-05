using System;
using System.Diagnostics;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using PersonalHomePage.Extensions;

namespace PersonalHomePage
{
    public class MvcApplication : HttpApplication
    {
        private readonly Lazy<TelemetryClient> _telemetryClient = new Lazy<TelemetryClient>(() => new TelemetryClient(), LazyThreadSafetyMode.ExecutionAndPublication);

        protected void Application_Start()
        {
            TelemetryConfiguration.Active.ContextInitializers.Add(new TelemetryBuildVersionContextInitializer());

            MvcHandler.DisableMvcResponseHeader = true;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            WriteExceptionToDebugConsole(exception);
            WriteExceptionToApplicationInsights(exception);
        }

        [Conditional("DEBUG")]
        private void WriteExceptionToDebugConsole(Exception exception)
        {
            Debug.WriteLine(exception.ToString());
        }

        private void WriteExceptionToApplicationInsights(Exception exception)
        {
            _telemetryClient.Value.TrackException(exception);
        }
    }
}
