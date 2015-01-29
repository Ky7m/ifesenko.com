using System.Web.Optimization;
using ResumePortfolioWebSite.Extensions;

namespace ResumePortfolioWebSite
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            //BundleTable.EnableOptimizations = true; //force optimization while debugging

            var jquery = new ScriptBundle("~/bundles/jquery",
                "//code.jquery.com/jquery-2.1.3.min.js")
                .Include("~/Scripts/jquery-{version}.js");
            jquery.CdnFallbackExpression = "window.jQuery";
            bundles.Add(jquery);

            var bootstrapJs = new ScriptBundle("~/bundles/bootstrapJs",
                "//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/js/bootstrap.min.js")
                .Include("~/Scripts/bootstrap.min.js");
            bootstrapJs.CdnFallbackExpression = "window.jQuery.fn.modal";
            bundles.Add(bootstrapJs);

            var respondJs = new ScriptBundle("~/bundles/respondJs",
                "//cdnjs.cloudflare.com/ajax/libs/respond.js/1.4.2/respond.js")
                .Include("~/Scripts/respond.min.js");
            respondJs.CdnFallbackExpression = "window.respond";
            bundles.Add(respondJs);


            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/jquery.countTo.js",
                "~/Scripts/jquery.simple-text-rotator.js",
                "~/Scripts/jquery.waypoints.js",
                "~/Scripts/owl.carousel.js",
                "~/Scripts/jquery.smooth.scroll-{version}.js",
                "~/Scripts/wow.js",
                "~/Scripts/jquery.backstretch.js",

                "~/Scripts/app/*.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrapCss",
               "//maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css")
               .IncludeFallback("~/Content/bootstrap.min.css", "sr-only", "width", "1px"));

            bundles.Add(new StyleBundle("~/bundles/font-awesome",
               "//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css")
               .IncludeFallback("~/Content/font-awesome.min.css", "fa", "font-family", "FontAwesome"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
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
