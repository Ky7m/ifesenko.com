using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using PersonalWebApp.Models;
using PersonalWebApp.Services.Interfaces;

namespace PersonalWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStorageService _storageService;

        public HomeController(IStorageService storageService) => _storageService = storageService;

        [Route("/")]
        [ResponseCache(CacheProfileName = "HomePage")]
        public async Task<IActionResult> Index()
        {
            var homeModel = await PopulateHomeModel();
            return View(homeModel);
        }

        private async Task<HomeModel> PopulateHomeModel()
        {
            return new HomeModel {Events = await _storageService.RetrieveAllEventsAsync()};
        }
    }
}