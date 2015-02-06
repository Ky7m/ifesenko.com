using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using PersonalHomePage.Extensions.IframeOptions;
using WebMarkupMin.Mvc.ActionFilters;

namespace PersonalHomePage.Controllers
{
    [NoIFrame]
    [OutputCache(VaryByParam = "*", Duration = 6000, Location = OutputCacheLocation.Downstream)]
    public class ErrorController : Controller
    {
        [CompressContent]
        [MinifyHtml]
        public ActionResult Index()
        {
            Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            return View();
        }

        [CompressContent]
        [MinifyHtml]
        public ActionResult NotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();
        }
    }
}
