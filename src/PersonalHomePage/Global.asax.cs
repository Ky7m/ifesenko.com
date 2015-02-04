using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PersonalHomePage
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //handle exceptions, send them via email, whatever
            var exception = Server.GetLastError();
        }
    }
}
