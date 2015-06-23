using System.Configuration;
using System.Web.Optimization;
using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using BundleTransformer.Core.Transformers;
using PersonalHomePage.Extensions;

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
            fontAwesomeBundle.Builder = nullBuilder;
            fontAwesomeBundle.Transforms.Add(styleTransformer);
            fontAwesomeBundle.Orderer = nullOrderer;
            bundles.Add(fontAwesomeBundle);

            var bootstrapCssBundle = new Bundle("~/bundles/bootstrap-css", "//cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/css/bootstrap.min.css");
            bootstrapCssBundle.Builder = nullBuilder;
            bootstrapCssBundle.Transforms.Add(styleTransformer);
            bootstrapCssBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapCssBundle);

            var commonStylesBundle = new Bundle("~/bundles/css", string.Format(cdnUrl, "bundles/css"));
            commonStylesBundle.Include(
                "~/Content/animate.css",
                        "~/Content/jquery.smooth.scroll.css",
                        "~/Content/OwlCarousel/owl.carousel.css",
                        "~/Content/OwlCarousel/owl.theme.css",
                        "~/Content/OwlCarousel/owl.transitions.css",
                        "~/Content/simpletextrotator.css",
                        "~/Content/toastr.css",
                        "~/Content/nprogress.css",
                        "~/Content/site.css");
            commonStylesBundle.Builder = nullBuilder;
            commonStylesBundle.Transforms.Add(styleTransformer);
            commonStylesBundle.Orderer = nullOrderer;
            bundles.Add(commonStylesBundle);

            //

            var jQueryBundle = new Bundle("~/bundles/jquery", "//cdnjs.cloudflare.com/ajax/libs/jquery/2.1.4/jquery.min.js");
            jQueryBundle.Include("~/Scripts/jquery-{version}.js");
            jQueryBundle.Builder = nullBuilder;
            jQueryBundle.Transforms.Add(scriptTransformer);
            jQueryBundle.Orderer = nullOrderer;
            jQueryBundle.CdnFallbackExpression = "window.jquery";
            bundles.Add(jQueryBundle);

            var bootstrapJsBundle = new Bundle("~/bundles/bootstrap-js", "//cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.5/js/bootstrap.min.js");
            bootstrapJsBundle.Include("~/Scripts/bootstrap.min.js");
            bootstrapJsBundle.Builder = nullBuilder;
            bootstrapJsBundle.Transforms.Add(scriptTransformer);
            bootstrapJsBundle.Orderer = nullOrderer;
            bootstrapJsBundle.CdnFallbackExpression = "$.fn.modal";
            bundles.Add(bootstrapJsBundle);

            var html5shivBundle = new Bundle("~/bundles/html5shiv", "//cdnjs.cloudflare.com/ajax/libs/html5shiv/3.7.2/html5shiv.min.js");
            html5shivBundle.Include("~/Scripts/html5shiv.min.js");
            html5shivBundle.Builder = nullBuilder;
            html5shivBundle.Transforms.Add(scriptTransformer);
            html5shivBundle.Orderer = nullOrderer;
            html5shivBundle.CdnFallbackExpression = "window.html5";
            bundles.Add(html5shivBundle);

            var respondJsBundle = new Bundle("~/bundles/respondJs", "//cdnjs.cloudflare.com/ajax/libs/respond.js/1.4.2/respond.min.js");
            respondJsBundle.Include("~/Scripts/respond.min.js");
            respondJsBundle.Builder = nullBuilder;
            respondJsBundle.Transforms.Add(scriptTransformer);
            respondJsBundle.Orderer = nullOrderer;
            respondJsBundle.CdnFallbackExpression = "window.respond";
            bundles.Add(respondJsBundle);

            var commonScriptsBundle = new Bundle("~/bundles/js", string.Format(cdnUrl, "bundles/js"));
            commonScriptsBundle.Include(
               "~/Scripts/jquery.simple-text-rotator.js",
                        "~/Scripts/owl.carousel.js",
                        "~/Scripts/jquery.smooth.scroll-{version}.js",
                        "~/Scripts/wow.js",
                        "~/Scripts/jquery.backstretch.js",
                        "~/Scripts/knockout-{version}.js",
                        "~/Scripts/knockout.validation.js",
                        "~/Scripts/toastr.js",
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