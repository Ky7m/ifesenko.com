﻿using System.Configuration;
using System.Reflection;
using System.Web.Optimization;

namespace PersonalHomePage
{
    public static class BundleConfig
    {
        public static string CdnUrl { get;  private set; }
        public static string Version { get;  private set; }
        public static string SiteJsBundleName { get; private set; }
        public static string SiteCssBundleName { get; private set; }

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

            var fontAwesomeBundle = new StyleBundle("~/bundles/font-awesome-css", "//cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css");
            fontAwesomeBundle.Include("~/Content/css/font-awesome.css");
            bundles.Add(fontAwesomeBundle);

            var bootstrapCssBundle = new StyleBundle("~/bundles/bootstrap-css", "//ajax.aspnetcdn.com/ajax/bootstrap/3.3.5/css/bootstrap.min.css");
            bootstrapCssBundle.Include("~/Content/bootstrap.css");
            bundles.Add(bootstrapCssBundle);

            var animateCssBundle = new StyleBundle("~/bundles/animate-css", "//cdnjs.cloudflare.com/ajax/libs/animate.css/3.4.0/animate.min.css");
            animateCssBundle.Include("~/Content/animate.css");
            bundles.Add(animateCssBundle);

            var siteCssBundle = new StyleBundle(SiteCssBundleName, string.Format(CdnUrl, SiteCssBundleName.TrimStart("~/".ToCharArray())));
            siteCssBundle.Include("~/Content/site.css");
            bundles.Add(siteCssBundle);

            //

            var aiBundle = new ScriptBundle("~/bundles/ai-js", string.Format(CdnUrl, "Scripts/ai.0.15.0-build58334.min.js"));
            aiBundle.Include("~/Scripts/ai.0.15.0-build58334.min.js");
            bundles.Add(aiBundle);

            var jQueryBundle = new ScriptBundle("~/bundles/jquery-js", "//ajax.aspnetcdn.com/ajax/jQuery/jquery-2.1.4.min.js");
            jQueryBundle.Include("~/Scripts/jquery-{version}.js");
            jQueryBundle.CdnFallbackExpression = "window.jQuery";
            bundles.Add(jQueryBundle);

            var bootstrapJsBundle = new ScriptBundle("~/bundles/bootstrap-js", "//ajax.aspnetcdn.com/ajax/bootstrap/3.3.5/bootstrap.min.js");
            bootstrapJsBundle.Include("~/Scripts/bootstrap.js");
            bootstrapJsBundle.CdnFallbackExpression = "$.fn.modal";
            bundles.Add(bootstrapJsBundle);

            var html5shivBundle = new ScriptBundle("~/bundles/html5shiv-js", "//cdnjs.cloudflare.com/ajax/libs/html5shiv/3.7.3/html5shiv.min.js");
            html5shivBundle.Include("~/Scripts/html5shiv.js");
            html5shivBundle.CdnFallbackExpression = "window.html5";
            bundles.Add(html5shivBundle);

            var respondJsBundle = new ScriptBundle("~/bundles/respond-js", "//cdnjs.cloudflare.com/ajax/libs/respond.js/1.4.2/respond.min.js");
            respondJsBundle.Include("~/Scripts/respond.js");
            respondJsBundle.CdnFallbackExpression = "window.respond";
            bundles.Add(respondJsBundle);

            var knockoutJsBundle = new ScriptBundle("~/bundles/knockout", "//ajax.aspnetcdn.com/ajax/knockout/knockout-3.3.0.js");
            knockoutJsBundle.Include("~/Scripts/knockout-{version}.js");
            knockoutJsBundle.CdnFallbackExpression = "window.ko";
            bundles.Add(knockoutJsBundle);

            var wowJsBundle = new ScriptBundle("~/bundles/wow-js", "//cdnjs.cloudflare.com/ajax/libs/wow/1.1.2/wow.min.js");
            wowJsBundle.Include("~/Scripts/wow.js");
            bundles.Add(wowJsBundle);

            var backstretchJsBundle = new ScriptBundle("~/bundles/backstretch-js", "//cdnjs.cloudflare.com/ajax/libs/jquery-backstretch/2.0.4/jquery.backstretch.min.js");
            backstretchJsBundle.Include("~/Scripts/jquery.backstretch.js");
            bundles.Add(backstretchJsBundle);

            var siteJsBundle = new ScriptBundle(SiteJsBundleName, string.Format(CdnUrl, SiteJsBundleName.TrimStart("~/".ToCharArray())));
            siteJsBundle.Include(
                "~/Scripts/app/extensions/*.js",
                
                "~/Scripts/app/bindingHandlers/*.js",

                "~/Scripts/app/models/*.js",
                "~/Scripts/app/viewModels/*.js",

                "~/Scripts/app/shell.js"
                );
            bundles.Add(siteJsBundle);
        }
    }
}