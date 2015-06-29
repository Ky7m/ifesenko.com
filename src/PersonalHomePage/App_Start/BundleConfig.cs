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
            bundles.UseCdn = true;
            var version = Assembly.GetAssembly(typeof(BundleConfig)).GetName().Version.ToString();
            var cdnUrl = ConfigurationManager.AppSettings.Get("CdnUrl") + "/{0}?v=" + version;
            var cdnUrlWithoutVersion = ConfigurationManager.AppSettings.Get("CdnUrl") + "/{0}";

            var fontAwesomeBundle = new StyleBundle("~/bundles/font-awesome-css", "//cdnjs.cloudflare.com/ajax/libs/font-awesome/4.3.0/css/font-awesome.min.css");
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

            var textRotatorCssBundle = new StyleBundle("~/bundles/simpletextrotator-css", "//cdnjs.cloudflare.com/ajax/libs/simple-text-rotator/1.0.0/simpletextrotator.min.css");
            textRotatorCssBundle.Include("~/Content/simpletextrotator.css");
            bundles.Add(textRotatorCssBundle);

            var nprogressCssBundle = new StyleBundle("~/bundles/nprogress-css", "//cdnjs.cloudflare.com/ajax/libs/nprogress/0.2.0/nprogress.min.css");
            nprogressCssBundle.Include("~/Content/nprogress.css");
            bundles.Add(nprogressCssBundle);

            var siteCssBundle = new StyleBundle("~/bundles/site-css", string.Format(cdnUrl, "bundles/site-css"));
            siteCssBundle.Include("~/Content/site.css");
            bundles.Add(siteCssBundle);

            //

            var aiBundle = new ScriptBundle("~/bundles/ai-js", string.Format(cdnUrlWithoutVersion, "Scripts/ai.0.15.0-build28683.min.js"));
            aiBundle.Include("~/Scripts/ai.0.15.0-build28683.min.js");
            bundles.Add(aiBundle);

            var jQueryBundle = new ScriptBundle("~/bundles/jquery-js", "//cdnjs.cloudflare.com/ajax/libs/jquery/2.1.4/jquery.min.js");
            jQueryBundle.Include("~/Scripts/jquery-{version}.js");
            jQueryBundle.CdnFallbackExpression = "window.jQuery";
            bundles.Add(jQueryBundle);

            var bootstrapJsBundle = new ScriptBundle("~/bundles/bootstrap-js", "//cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/js/bootstrap.min.js");
            bootstrapJsBundle.Include("~/Scripts/bootstrap.js");
            bootstrapJsBundle.CdnFallbackExpression = "$.fn.modal";
            bundles.Add(bootstrapJsBundle);

            var html5shivBundle = new ScriptBundle("~/bundles/html5shiv-js", "//cdnjs.cloudflare.com/ajax/libs/html5shiv/3.7.2/html5shiv.min.js");
            html5shivBundle.Include("~/Scripts/html5shiv.js");
            html5shivBundle.CdnFallbackExpression = "window.html5";
            bundles.Add(html5shivBundle);

            var respondJsBundle = new ScriptBundle("~/bundles/respond-js", "//cdnjs.cloudflare.com/ajax/libs/respond.js/1.4.2/respond.min.js");
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

            var textRotatorJsBundle = new ScriptBundle("~/bundles/simpletextrotator-js", "//cdnjs.cloudflare.com/ajax/libs/simple-text-rotator/1.0.0/jquery.simple-text-rotator.min.js");
            textRotatorJsBundle.Include("~/Scripts/jquery.simple-text-rotator.js");
            bundles.Add(textRotatorJsBundle);

            var wowJsBundle = new ScriptBundle("~/bundles/wow-js", "//cdnjs.cloudflare.com/ajax/libs/wow/1.1.2/wow.min.js");
            wowJsBundle.Include("~/Scripts/wow.js");
            bundles.Add(wowJsBundle);

            var backstretchJsBundle = new ScriptBundle("~/bundles/backstretch-js", "//cdnjs.cloudflare.com/ajax/libs/jquery-backstretch/2.0.4/jquery.backstretch.min.js");
            backstretchJsBundle.Include("~/Scripts/jquery.backstretch.js");
            bundles.Add(backstretchJsBundle);

            var knockoutValidationJsBundle = new ScriptBundle("~/bundles/knockout-validation-js", "//cdnjs.cloudflare.com/ajax/libs/knockout-validation/2.0.3/knockout.validation.min.js");
            knockoutValidationJsBundle.Include("~/Scripts/knockout.validation.js");
            bundles.Add(knockoutValidationJsBundle);

            var nprogressJsBundle = new ScriptBundle("~/bundles/nprogress-js", "//cdnjs.cloudflare.com/ajax/libs/nprogress/0.2.0/nprogress.min.js");
            nprogressJsBundle.Include("~/Scripts/nprogress.js");
            bundles.Add(nprogressJsBundle);

            var siteJsBundle = new ScriptBundle("~/bundles/site-js", string.Format(cdnUrl, "bundles/site-js"));
            siteJsBundle.Include(
                "~/Scripts/bindingHandlers/*.js",

                "~/Scripts/app/helpers/*.js",

                "~/Scripts/app/base/*.js",
                "~/Scripts/app/models/*.js",
                "~/Scripts/app/viewModels/*.js",

                "~/Scripts/app/shell.js"
                );
            bundles.Add(siteJsBundle);


        }
    }
}