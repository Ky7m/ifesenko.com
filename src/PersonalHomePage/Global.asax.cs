using System;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ChameleonForms;
using ChameleonForms.ModelBinders;
using ChameleonForms.Templates.TwitterBootstrap3;
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
            ConfigureViewEngines();
            ConfigureAntiForgeryTokens();
            RegisterChameleonFormsComponents();

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
            WriteExceptionToApplicationInsights(exception);
        }

        /// <summary>
        /// Configures the view engines. By default, Asp.Net MVC includes the Web Forms (WebFormsViewEngine) and 
        /// Razor (RazorViewEngine) view engines. You can remove view engines you are not using here for better
        /// performance.
        /// </summary>
        private static void ConfigureViewEngines()
        {
            // Only use the RazorViewEngine.
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        /// <summary>
        /// Configures the anti-forgery tokens.
        /// </summary>
        private static void ConfigureAntiForgeryTokens()
        {
            // Rename the Anti-Forgery cookie from "__RequestVerificationToken" to "f". 
            // This adds a little security through obscurity and also saves sending a 
            // few characters over the wire.
            AntiForgeryConfig.CookieName = "f";

            // If you have enabled SSL. Uncomment this line to ensure that the Anti-Forgery 
            // cookie requires SSL to be sent accross the wire. 
            // AntiForgeryConfig.RequireSsl = true;
        }

        private void WriteExceptionToApplicationInsights(Exception exception)
        {
            _telemetryClient.Value.TrackException(exception);
        }

        private static void RegisterChameleonFormsComponents()
        {
            FormTemplate.Default = new TwitterBootstrapFormTemplate();
            ModelBinders.Binders.Add(typeof (DateTime), new DateTimeModelBinder());
            ModelBinders.Binders.Add(typeof (DateTime?), new DateTimeModelBinder());
        }
    }
}
