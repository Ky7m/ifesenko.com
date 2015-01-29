using System.Web.Mvc;
using System.Web.UI;
using ResumePortfolioWebSite.Extensions.IframeOptions;
using ResumePortfolioWebSite.Models;
using WebMarkupMin.Mvc.ActionFilters;

namespace ResumePortfolioWebSite.Controllers
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