using System.Web.Mvc;
using NWebsec.Mvc.HttpHeaders;

namespace PersonalHomePage
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            AddSecurityFilters(filters);
        }

        /// <summary>
        /// Several NWebsec Security Filters are added here. 
        /// (See <see cref="http://rehansaeed.co.uk/nwebsec-asp-net-mvc-security-through-http-headers/"/>, 
        /// <see cref="http://www.dotnetnoob.com/2012/09/security-through-http-response-headers.html"/> and 
        /// <see cref="https://github.com/NWebsec/NWebsec/wiki"/> for more information).
        /// Note: All of these filters can be applied to individual controllers and actions and indeed
        /// some of them only make sense when applied to a controller or action instead of globally here.
        /// </summary>
        private static void AddSecurityFilters(GlobalFilterCollection filters)
        {
            // Require HTTPS to be used accross the whole site. System.Web.Mvc.RequireHttpsAttribute performs a 302 Temporary redirect 
            // from a HTTP URL to a HTTPS URL. This filter gives you the option to perform a 301 Permanent redirect or a 302 temporary redirect.
            // You should perform a 301 permanent redirect if the page can only ever be accessed by HTTPS and a 302 temporary redirect if
            // the page can be accessed over HTTP or HTTPS.
            // filters.Add(new RedirectToHttpsAttribute(true));

            // Cache-Control: no-cache, no-store, must-revalidate
            // Expires: -1
            // Pragma: no-cache
            //      Specifies whether appropriate headers to prevent browser caching should be set in the HTTP response.
            //      Do not apply this attribute here globally, use it sparingly to disable caching. A good place to 
            //      use this would be on a page where you want to post back credit card information because caching 
            //      credit card information could be a security risk.
            // filters.Add(new SetNoCacheHttpHeadersAttribute());   

            // X-Robots-Tag - Adds the X-Robots-Tag HTTP header. Disable robots from any action or controller this attribute is applied to.
            // filters.Add(new XRobotsTagAttribute() { NoIndex = true, NoFollow = true }); 

            // X-Content-Type-Options - Adds the X-Content-Type-Options HTTP header. Stop IE9 and below from sniffing files and overriding the Content-Type header (MIME type).
            filters.Add(new XContentTypeOptionsAttribute());

            // X-Download-Options - Adds the X-Download-Options HTTP header. When users save the page, stops them from opening it and forces a save and manual open.
            filters.Add(new XDownloadOptionsAttribute());

            // X-Frame-Options - Adds the X-Frame-Options HTTP header. Stop clickjacking by stopping the page from opening in an iframe or only allowing it from the same origin.  
            //      Deny - Specifies that the X-Frame-Options header should be set in the HTTP response, instructing the browser to display the page when it is loaded in an iframe - but only if the iframe is from the same origin as the page.
            //      SameOrigin - Specifies that the X-Frame-Options header should be set in the HTTP response, instructing the browser to not display the page when it is loaded in an iframe.
            //      Disabled - Specifies that the X-Frame-Options header should not be set in the HTTP response.
            filters.Add(new XFrameOptionsAttribute
            {
                Policy = XFrameOptionsPolicy.Deny
            });
        }
    }
}
