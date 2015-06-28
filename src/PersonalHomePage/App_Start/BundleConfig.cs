using System.Configuration;
using System.Web.Optimization;
using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using BundleTransformer.Core.Transformers;

namespace PersonalHomePage
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = true; //force optimization while debugging
            bundles.UseCdn = true;
            var version = System.Reflection.Assembly.GetAssembly(typeof(BundleConfig)).GetName().Version.ToString();
            var cdnUrl = ConfigurationManager.AppSettings.Get("CdnUrl") + "/{0}?v=" + version;


            var nullBuilder = new NullBuilder();
            var styleTransformer = new StyleTransformer();
            var scriptTransformer = new ScriptTransformer();
            var nullOrderer = new NullOrderer();

            // Replace a default bundle resolver in order to the debugging HTTP-handler
            // can use transformations of the corresponding bundle
            BundleResolver.Current = new CustomBundleResolver();

            var fontAwesomeBundle = new Bundle("~/bundles/font-awesome", "//cdnjs.cloudflare.com/ajax/libs/font-awesome/4.3.0/css/font-awesome.min.css");
            fontAwesomeBundle.Include("~/Content/css/font-awesome.css");
            fontAwesomeBundle.Builder = nullBuilder;
            fontAwesomeBundle.Transforms.Add(styleTransformer);
            fontAwesomeBundle.Orderer = nullOrderer;
            bundles.Add(fontAwesomeBundle);

            var bootstrapCssBundle = new Bundle("~/bundles/bootstrap-css", "//cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css");
            bootstrapCssBundle.Include("~/Content/bootstrap.css");
            bootstrapCssBundle.Builder = nullBuilder;
            bootstrapCssBundle.Transforms.Add(styleTransformer);
            bootstrapCssBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapCssBundle);

            var animateCssBundle = new Bundle("~/bundles/animate-css", "//cdnjs.cloudflare.com/ajax/libs/animate.css/3.3.0/animate.min.css");
            animateCssBundle.Include("~/Content/animate.css");
            animateCssBundle.Builder = nullBuilder;
            animateCssBundle.Transforms.Add(styleTransformer);
            animateCssBundle.Orderer = nullOrderer;
            bundles.Add(animateCssBundle);

            var toastrCssBundle = new Bundle("~/bundles/toastr-css", "//cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.1/toastr.min.css");
            toastrCssBundle.Include("~/Content/toastr.css");
            toastrCssBundle.Builder = nullBuilder;
            toastrCssBundle.Transforms.Add(styleTransformer);
            toastrCssBundle.Orderer = nullOrderer;
            bundles.Add(toastrCssBundle);

            var commonStylesBundle = new Bundle("~/bundles/css", string.Format(cdnUrl, "bundles/css"));
            commonStylesBundle.Include(
                "~/Content/jquery.smooth.scroll.css",
                "~/Content/simpletextrotator.css",
                "~/Content/nprogress.css",
                "~/Content/site.css");
            commonStylesBundle.Builder = nullBuilder;
            commonStylesBundle.Transforms.Add(styleTransformer);
            commonStylesBundle.Orderer = nullOrderer;
            bundles.Add(commonStylesBundle);

            //

            var aiBundle = new Bundle("~/bundles/ai", string.Format(cdnUrl, "bundles/ai"));
            aiBundle.Include("~/Scripts/ai.0.15.0-build28683.js");
            aiBundle.Builder = nullBuilder;
            aiBundle.Transforms.Add(scriptTransformer);
            aiBundle.Orderer = nullOrderer;
            aiBundle.CdnFallbackExpression = "window.ApplicationInsights";
            bundles.Add(aiBundle);

            var jQueryBundle = new Bundle("~/bundles/jquery", "//cdnjs.cloudflare.com/ajax/libs/jquery/2.1.4/jquery.min.js");
            jQueryBundle.Include("~/Scripts/jquery-{version}.js");
            jQueryBundle.Builder = nullBuilder;
            jQueryBundle.Transforms.Add(scriptTransformer);
            jQueryBundle.Orderer = nullOrderer;
            jQueryBundle.CdnFallbackExpression = "window.jQuery";
            bundles.Add(jQueryBundle);

            var bootstrapJsBundle = new Bundle("~/bundles/bootstrap-js", "//cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/js/bootstrap.min.js");
            bootstrapJsBundle.Include("~/Scripts/bootstrap.js");
            bootstrapJsBundle.Builder = nullBuilder;
            bootstrapJsBundle.Transforms.Add(scriptTransformer);
            bootstrapJsBundle.Orderer = nullOrderer;
            bootstrapJsBundle.CdnFallbackExpression = "$.fn.modal";
            bundles.Add(bootstrapJsBundle);

            var html5shivBundle = new Bundle("~/bundles/html5shiv", "//cdnjs.cloudflare.com/ajax/libs/html5shiv/3.7.2/html5shiv.min.js");
            html5shivBundle.Include("~/Scripts/html5shiv.js");
            html5shivBundle.Builder = nullBuilder;
            html5shivBundle.Transforms.Add(scriptTransformer);
            html5shivBundle.Orderer = nullOrderer;
            html5shivBundle.CdnFallbackExpression = "window.html5";
            bundles.Add(html5shivBundle);

            var respondJsBundle = new Bundle("~/bundles/respondJs", "//cdnjs.cloudflare.com/ajax/libs/respond.js/1.4.2/respond.min.js");
            respondJsBundle.Include("~/Scripts/respond.js");
            respondJsBundle.Builder = nullBuilder;
            respondJsBundle.Transforms.Add(scriptTransformer);
            respondJsBundle.Orderer = nullOrderer;
            respondJsBundle.CdnFallbackExpression = "window.respond";
            bundles.Add(respondJsBundle);

            var knockoutJsBundle = new Bundle("~/bundles/knockout", "//cdnjs.cloudflare.com/ajax/libs/knockout/3.3.0/knockout-min.js");
            knockoutJsBundle.Include("~/Scripts/knockout-{version}.js");
            knockoutJsBundle.Builder = nullBuilder;
            knockoutJsBundle.Transforms.Add(scriptTransformer);
            knockoutJsBundle.Orderer = nullOrderer;
            knockoutJsBundle.CdnFallbackExpression = "window.ko";
            bundles.Add(knockoutJsBundle);

            var toastrJsBundle = new Bundle("~/bundles/toastr-js", "//cdnjs.cloudflare.com/ajax/libs/toastr.js/2.1.1/toastr.min.js");
            toastrJsBundle.Include("~/Scripts/toastr.js");
            toastrJsBundle.Builder = nullBuilder;
            toastrJsBundle.Transforms.Add(scriptTransformer);
            toastrJsBundle.Orderer = nullOrderer;
            bundles.Add(toastrJsBundle);

            var commonScriptsBundle = new Bundle("~/bundles/js", string.Format(cdnUrl, "bundles/js"));
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

                "~/Scripts/app/shell.js");


            commonStylesBundle.Builder = nullBuilder;
            commonScriptsBundle.Transforms.Add(scriptTransformer);
            commonScriptsBundle.Orderer = nullOrderer;
            bundles.Add(commonScriptsBundle);
        }
    }
}