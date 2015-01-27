using System.Web.Mvc;
using ResumePortfolioWebSite.Models;
using WebMarkupMin.Mvc.ActionFilters;

namespace ResumePortfolioWebSite.Controllers
{
    public class HomeController : Controller
    {
        [CompressContent]
        [MinifyHtml]
        [OutputCache(Duration = 600)]
        public ActionResult Index()
        {
            return View(new HomeModel());
        }
    }
}