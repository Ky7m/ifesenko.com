using System.Web.Optimization;

namespace ResumePortfolioWebSite
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(

                "~/Scripts/jquery-{version}.js",

                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",

                "~/Scripts/jquery.countTo.js",
                "~/Scripts/jquery.simple-text-rotator.js",
                "~/Scripts/jquery.waypoints.js",
                "~/Scripts/owl.carousel.js",
                "~/Scripts/jquery.smooth.scroll-{version}.js",
                "~/Scripts/wow.js",
                "~/Scripts/jquery.backstretch.js",

                "~/Scripts/app/*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/animate.css",
                      "~/Content/font-awesome.css",
                      "~/Content/jquery.smooth.scroll.css",
                      "~/Content/OwlCarousel/owl.carousel.css",
                      "~/Content/OwlCarousel/owl.theme.css",
                      "~/Content/OwlCarousel/owl.transitions.css",
                      "~/Content/simpletextrotator.css",
                      "~/Content/site.css"));
        }
    }
}
