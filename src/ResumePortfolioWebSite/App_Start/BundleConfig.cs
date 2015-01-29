using System.Configuration;
using System.Web.Optimization;
using ResumePortfolioWebSite.Extensions;

namespace ResumePortfolioWebSite
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = true; //force optimization while debugging

            bundles.UseCdn = true;
            var version = System.Reflection.Assembly.GetAssembly(typeof (BundleConfig)).GetName().Version.ToString();
            var cdnUrl = ConfigurationManager.AppSettings.Get("CdnUrl")+ "/{0}?v=" + version;

            bundles.Add(
                new ScriptBundle("~/bundles/jquery",
                    "//code.jquery.com/jquery-2.1.3.min.js") {CdnFallbackExpression = "window.jQuery"}
                    .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/bootstrapJs",
                    "//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js") { CdnFallbackExpression = "window.jQuery.fn.modal" }
                    .Include("~/Scripts/bootstrap.min.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/html5shiv",
                    "//oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js") {CdnFallbackExpression = "window.html5"}
                    .Include("~/Scripts/html5shiv.min.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/respondJs",
                    "//oss.maxcdn.com/respond/1.4.2/respond.min.js") {CdnFallbackExpression = "window.respond"}
                    .Include("~/Scripts/respond.min.js"));


            bundles.Add(new StyleBundle("~/bundles/bootstrapCss",
               "//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css")
               .IncludeFallback("~/Content/bootstrap.min.css", "sr-only", "width", "1px"));

            bundles.Add(new StyleBundle("~/bundles/font-awesome",
               "//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css")
               .IncludeFallback("~/Content/font-awesome.min.css", "fa", "font-family", "FontAwesome"));


            bundles.Add(
                new ScriptBundle("~/bundles/js",
                    string.Format(cdnUrl, "bundles/js")) { CdnFallbackExpression = "window.jQuery.fn.waypoint" }
                    .Include(
                        "~/Scripts/jquery.countTo.js",
                        "~/Scripts/jquery.simple-text-rotator.js",
                        "~/Scripts/jquery.waypoints.js",
                        "~/Scripts/owl.carousel.js",
                        "~/Scripts/jquery.smooth.scroll-{version}.js",
                        "~/Scripts/wow.js",
                        "~/Scripts/jquery.backstretch.js",

                        "~/Scripts/app/*.js"));


            bundles.Add(
                new StyleBundle("~/bundles/css",
                    string.Format(cdnUrl, "bundles/css")).IncludeFallback("~/bundles/css", "skill-bar", "height", "4px")
                    .Include(
                        "~/Content/animate.css",
                        "~/Content/jquery.smooth.scroll.css",
                        "~/Content/OwlCarousel/owl.carousel.css",
                        "~/Content/OwlCarousel/owl.theme.css",
                        "~/Content/OwlCarousel/owl.transitions.css",
                        "~/Content/simpletextrotator.css",
                        "~/Content/site.css"));
        }
    }
}
