using System.Web.Optimization;

namespace ResumePortfolioWebSite
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
					  "~/Scripts/jquery.countTo.js",
					  "~/Scripts/jquery.fitvids.js",
					  "~/Scripts/jquery.magnific-popup.min.js",
					  "~/Scripts/jquery.simple-text-rotator.min.js",
					  "~/Scripts/jquery.waypoints.js",
					  "~/Scripts/owl.carousel.min.js",
					  "~/Scripts/smoothscroll.js",
					  "~/Scripts/wow.min.js",
					  "~/Scripts/jquery.backstretch.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/app").Include("~/Scripts/app/*.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/animate.css",
					  "~/Content/font-awesome.min.css",
					  "~/Content/magnific-popup.css",
					  "~/Content/owl.carousel.css",
					  "~/Content/owl.theme.css",
					  "~/Content/simpletextrotator.css",
					  "~/Content/site.css"));
		}
	}
}
