using System;
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
        private readonly TelemetryClient _telemetryClient;

        public HomeController(IStorageService storageService,
            TelemetryClient telemetryClient) =>
            (_storageService, _telemetryClient) = (storageService, telemetryClient);

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