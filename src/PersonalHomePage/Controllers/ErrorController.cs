using System.Net;
using System.Web.Mvc;
using WebMarkupMin.Mvc.ActionFilters;

namespace PersonalHomePage.Controllers
{
    public class ErrorController : Controller
    {
        [CompressContent, MinifyHtml, OutputCache(CacheProfile = "InternalServerError")]
        public ActionResult Index()
        {
            Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            return View();
        }

        [CompressContent, MinifyHtml, OutputCache(CacheProfile = "NotFound")]
        public ActionResult NotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();
        }
    }
}
