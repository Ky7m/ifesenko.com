using System.Web.Mvc;
using PersonalHomePage.Extensions.IframeOptions;
using PersonalHomePage.Models;
using WebMarkupMin.Mvc.ActionFilters;

namespace PersonalHomePage.Controllers
{
    public class HomeController : Controller
    {
        [CompressContent,
         MinifyHtml,
         OutputCache(CacheProfile = "HomePage"),
         NoIFrame]
        public ActionResult Index()
        {
            return View(new HomeModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendEmailMessage(EmailMessageModel emailMessage)
        {
            return new JsonResult {Data = true};
        }
    }
}