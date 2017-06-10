using Microsoft.AspNetCore.Mvc;
using PersonalWebApp.Models;
using PersonalWebApp.Services;

namespace PersonalWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStorageService _storageService;

        public HomeController(IStorageService storageService) => _storageService = storageService;

        [Route("/")]
        [ResponseCache(CacheProfileName = "HomePage")]
        public IActionResult Index(string period)
        {
            var result = _storageService.RetrieveEventsForPeriod(period);
            var homeModel = new HomeModel {Events = result.Events, IsItAllEvents = result.IsItAllEvents};
            return View(homeModel);
        }
    }
}