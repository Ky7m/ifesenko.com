using System.Configuration;
using System.Reflection;
using System.Web.Optimization;

namespace PersonalHomePage
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true; //force optimization while debugging
            //bundles.UseCdn = true;
            var version = Assembly.GetAssembly(typeof(BundleConfig)).GetName().Version.ToString();
            var cdnUrl = ConfigurationManager.AppSettings.Get("CdnUrl") + "/{0}?v=" + version;

            var fontAwesomeBundle = new StyleBundle("~/bundles/font-awesome", "//cdnjs.cloudflare.com/ajax/libs/font-awesome/4.3.0/css/font-awesome.min.css");
            fontAwesomeBundle.Include("~/Content/css/font-awesome.css");
            bundles.Add(fontAwesomeBundle);

            var bootstrapCssBundle = new StyleBundle("~/bundles/bootstrap-css", "//cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css");
            bootstrapCssBundle.Include("~/Content/bootstrap.css");
            bundles.Add(bootstrapCssBundle);

            var animateCssBundle = new StyleBundle("~/bundles/animate-css", "//cdnjs.cloudflare.com/ajax/libs/animate.css/3.3.0/animate.min.css");
            animateCssBundle.Include("~/Content/animate.css");
            bundles.Add(animateCssBundle);

            var toastrCssBundle = new StyleBundle("~/bundles/toastr-css", "//cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.1/toastr.min.css");
            toastrCssBundle.Include("~/Content/toastr.css");
            bundles.Add(toastrCssBundle);

            var commonStylesBundle = new StyleBundle("~/bundles/css", string.Format(cdnUrl, "bundles/css"));
            commonStylesBundle.Include(
                "~/Content/jquery.smooth.scroll.css",
                "~/Content/simpletextrotator.css",
                "~/Content/nprogress.css",
                "~/Content/site.css");
            bundles.Add(commonStylesBundle);

            //

            var aiBundle = new ScriptBundle("~/bundles/ai", string.Format(cdnUrl, "bundles/ai"));
            aiBundle.Include("~/Scripts/ai.0.15.0-build28683.js");
            aiBundle.CdnFallbackExpression = "window.ApplicationInsights";
            bundles.Add(aiBundle);

            var jQueryBundle = new ScriptBundle("~/bundles/jquery", "//cdnjs.cloudflare.com/ajax/libs/jquery/2.1.4/jquery.min.js");
            jQueryBundle.Include("~/Scripts/jquery-{version}.js");
            jQueryBundle.CdnFallbackExpression = "window.jQuery";
            bundles.Add(jQueryBundle);

            var bootstrapJsBundle = new ScriptBundle("~/bundles/bootstrap-js", "//cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/js/bootstrap.min.js");
            bootstrapJsBundle.Include("~/Scripts/bootstrap.js");
            bootstrapJsBundle.CdnFallbackExpression = "$.fn.modal";
            bundles.Add(bootstrapJsBundle);

            var html5shivBundle = new ScriptBundle("~/bundles/html5shiv", "//cdnjs.cloudflare.com/ajax/libs/html5shiv/3.7.2/html5shiv.min.js");
            html5shivBundle.Include("~/Scripts/html5shiv.js");
            html5shivBundle.CdnFallbackExpression = "window.html5";
            bundles.Add(html5shivBundle);

            var respondJsBundle = new ScriptBundle("~/bundles/respondJs", "//cdnjs.cloudflare.com/ajax/libs/respond.js/1.4.2/respond.min.js");
            respondJsBundle.Include("~/Scripts/respond.js");
            respondJsBundle.CdnFallbackExpression = "window.respond";
            bundles.Add(respondJsBundle);

            var knockoutJsBundle = new ScriptBundle("~/bundles/knockout", "//cdnjs.cloudflare.com/ajax/libs/knockout/3.3.0/knockout-min.js");
            knockoutJsBundle.Include("~/Scripts/knockout-{version}.js");
            knockoutJsBundle.CdnFallbackExpression = "window.ko";
            bundles.Add(knockoutJsBundle);

            var toastrJsBundle = new ScriptBundle("~/bundles/toastr-js", "//cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.1/toastr.min.js");
            toastrJsBundle.Include("~/Scripts/toastr.js");
            bundles.Add(toastrJsBundle);

            var commonScriptsBundle = new ScriptBundle("~/bundles/js", string.Format(cdnUrl, "bundles/js"));
            commonScriptsBundle.Include(
                "~/Scripts/jquery.simple-text-rotator.js",
                "~/Scripts/jquery.smooth.scroll-{version}.js",
                "~/Scripts/wow.js",
                "~/Scripts/jquery.backstretch.js",
                "~/Scripts/knockout.validation.js",
                "~/Scripts/nprogress.js",

                "~/Scripts/bindingHandlers/*.js",

                "~/Scripts/app/helpers/*.js",

                "~/Scripts/app/base/*.js",
                "~/Scripts/app/models/*.js",
                "~/Scripts/app/viewModels/*.js",

                "~/Scripts/app/shell.js"
                );
            bundles.Add(commonScriptsBundle);

            
        }
    }
}