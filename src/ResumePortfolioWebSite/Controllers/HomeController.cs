using System.Web.Mvc;
using ResumePortfolioWebSite.Models;

namespace ResumePortfolioWebSite.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View(new HomeModel());
		}
	}
}