using System.Web.Mvc;
using System.Web.Routing;

namespace PersonalHomePage
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Imprive SEO by stopping duplicate URL's due to case or trailing slashes.
            routes.AppendTrailingSlash = true;
            routes.LowercaseUrls = true;

            // IgnoreRoute - Tell the routing system to ignore certain routes for better performance.
            // Ignore .axd files.
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // Ignore everything in the Content folder.
            routes.IgnoreRoute("Content/{*pathInfo}");
            // Ignore everything in the Scripts folder.
            routes.IgnoreRoute("Scripts/{*pathInfo}");
            // Ignore the humans.txt file.
            routes.IgnoreRoute("humans.txt");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );

            routes.MapRoute(
                name: "ShortUrls",
                url: "go/{shortUrl}",
                defaults: new { controller = "Home", action = "RedirectToLong", shortUrl = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Basic",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", id = UrlParameter.Optional }
            );
        }
    }
}
