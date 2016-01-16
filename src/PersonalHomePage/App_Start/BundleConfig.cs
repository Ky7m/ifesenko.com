using System.Configuration;
using System.Reflection;
using System.Web.Optimization;

namespace PersonalHomePage
{
    public static class BundleConfig
    {
        public static string CdnUrl { get; }
        public static string Version { get; }
        public static string SiteJsBundleName { get; }
        public static string SiteCssBundleName { get; }

        static BundleConfig()
        {
            //BundleTable.EnableOptimizations = true; //force optimization while debugging

            var version = Assembly.GetAssembly(typeof(BundleConfig)).GetName().Version;
            Version = $"{version.Major}.{version.Minor}.{version.Build}";
            CdnUrl = ConfigurationManager.AppSettings.Get("CdnUrl") + "/{0}";
            var bundleVersion = Version.Replace(".", "-");
            SiteCssBundleName = $"~/bundles/site-css-{bundleVersion}";
            SiteJsBundleName = $"~/bundles/site-js-{bundleVersion}";
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            var fontAwesomeBundle = new StyleBundle("~/bundles/font-awesome-css", "//cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css");
            fontAwesomeBundle.Include("~/Content/css/font-awesome.css");
            bundles.Add(fontAwesomeBundle);

            var bootstrapCssBundle = new StyleBundle("~/bundles/bootstrap-css", "//cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.6/css/bootstrap.min.css");
            bootstrapCssBundle.Include("~/Content/bootstrap.css");
            bundles.Add(bootstrapCssBundle);

            var siteCssBundle = new StyleBundle(SiteCssBundleName, string.Format(CdnUrl, SiteCssBundleName.TrimStart("~/".ToCharArray())));
            siteCssBundle.Include("~/Content/site.css");
            bundles.Add(siteCssBundle);

            //

            var aiBundle = new ScriptBundle("~/bundles/ai-js", string.Format(CdnUrl, "Scripts/ai.0.21.5-build00175.min.js"));
            aiBundle.Include("~/Scripts/ai.0.21.5-build00175.min.js");
            bundles.Add(aiBundle);

            var jQueryBundle = new ScriptBundle("~/bundles/jquery-js", "//cdnjs.cloudflare.com/ajax/libs/jquery/2.2.0/jquery.min.js");
            jQueryBundle.Include("~/Scripts/jquery-{version}.js");
            jQueryBundle.CdnFallbackExpression = "window.jQuery";
            bundles.Add(jQueryBundle);

            var bootstrapJsBundle = new ScriptBundle("~/bundles/bootstrap-js", "//cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.6/js/bootstrap.min.js");
            bootstrapJsBundle.Include("~/Scripts/bootstrap.js");
            bootstrapJsBundle.CdnFallbackExpression = "$.fn.modal";
            bundles.Add(bootstrapJsBundle);

            var knockoutJsBundle = new ScriptBundle("~/bundles/knockout", "//cdnjs.cloudflare.com/ajax/libs/knockout/3.4.0/knockout-min.js");
            knockoutJsBundle.Include("~/Scripts/knockout-{version}.js");
            knockoutJsBundle.CdnFallbackExpression = "window.ko";
            bundles.Add(knockoutJsBundle);

            var backstretchJsBundle = new ScriptBundle("~/bundles/backstretch-js", "//cdnjs.cloudflare.com/ajax/libs/jquery-backstretch/2.0.4/jquery.backstretch.min.js");
            backstretchJsBundle.Include("~/Scripts/jquery.backstretch.js");
            bundles.Add(backstretchJsBundle);

            var siteJsBundle = new ScriptBundle(SiteJsBundleName, string.Format(CdnUrl, SiteJsBundleName.TrimStart("~/".ToCharArray())));
            siteJsBundle.Include(
                "~/Scripts/app/extensions/*.js",
                
                "~/Scripts/app/bindingHandlers/*.js",

                "~/Scripts/app/viewModels/*.js",

                "~/Scripts/app/shell.js"
                );
            bundles.Add(siteJsBundle);
        }
    }
}