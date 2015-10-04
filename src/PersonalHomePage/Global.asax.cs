using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using PersonalHomePage.Extensions;
using PersonalHomePage.Services.Implementation;
using PersonalHomePage.Services.Implementation.CloudStorageService;
using PersonalHomePage.Services.Implementation.HealthService;
using PersonalHomePage.Services.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace PersonalHomePage
{
    public class MvcApplication : HttpApplication
    {
        readonly Lazy<TelemetryClient> _telemetryClient = new Lazy<TelemetryClient>(() => new TelemetryClient());

        protected void Application_Start()
        {
            ConfigureViewEngines();
            ConfigureAntiForgeryTokens();

#if DEBUG
            TelemetryConfiguration.Active.DisableTelemetry = true;
#endif

            // Set context properties using custom telemetry initializers
            TelemetryConfiguration.Active.ContextInitializers.Add(new ApplicationInsightsConfigInitializer());

            MvcHandler.DisableMvcResponseHeader = true;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureRegistrationMap();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            WriteExceptionToApplicationInsights(exception);
        }

        static void ConfigureViewEngines()
        {
            // Only use the RazorViewEngine.
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        void ConfigureRegistrationMap()
        {
            var container = new Container();

            container.Register<IHealthService, HealthService>(Lifestyle.Singleton);
            container.Register<ICacheService, RedisCacheService>(Lifestyle.Singleton);
            container.Register<ISettingsService, SettingsService>(Lifestyle.Singleton);
            container.Register<IStorageService, CloudStorageService>(Lifestyle.Singleton);

            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        static void ConfigureAntiForgeryTokens()
        {
            // Rename the Anti-Forgery cookie from "__RequestVerificationToken" to "f". 
            // This adds a little security through obscurity and also saves sending a 
            // few characters over the wire.
            AntiForgeryConfig.CookieName = "f";

            // If you have enabled SSL. Uncomment this line to ensure that the Anti-Forgery 
            // cookie requires SSL to be sent accross the wire. 
            // AntiForgeryConfig.RequireSsl = true;
        }

        void WriteExceptionToApplicationInsights(Exception exception)
        {
            _telemetryClient.Value.TrackException(exception);
        }
    }
}
