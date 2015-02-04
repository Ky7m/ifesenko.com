using System.Web.Mvc;
using System.Web.UI;
using PersonalHomePage.Extensions.IframeOptions;
using PersonalHomePage.Models;
using WebMarkupMin.Mvc.ActionFilters;

namespace PersonalHomePage.Controllers
{
    public class HomeController : Controller
    {
        [CompressContent]
        [MinifyHtml]
        [OutputCache(VaryByParam = "*", Duration = 600, Location = OutputCacheLocation.Downstream)]
        [NoIFrame]
        public ActionResult Index()
        {
            return View(new HomeModel());
        }
    }
}