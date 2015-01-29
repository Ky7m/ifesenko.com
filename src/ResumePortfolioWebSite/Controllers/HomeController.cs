using System.Web.Mvc;
using ResumePortfolioWebSite.Extensions.IframeOptions;
using ResumePortfolioWebSite.Models;
using WebMarkupMin.Mvc.ActionFilters;

namespace ResumePortfolioWebSite.Controllers
{
    public class HomeController : Controller
    {
        [CompressContent]
        [MinifyHtml]
        [OutputCache(Duration = 600)]
        [NoIFrame]
        public ActionResult Index()
        {
            return View(new HomeModel());
        }
    }
}