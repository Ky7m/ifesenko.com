using System;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
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
            TelemetryClient telemetryClient)
        {
            _storageService = storageService;
            _telemetryClient = telemetryClient;
        }

        [Route("/")]
        [ResponseCache(CacheProfileName = "HomePage")]
        public async Task<IActionResult> Index()
        {
            var homeModel = await PopulateHomeModel();
            return View(homeModel);
        }

        [Route("go/{shortUrl?}")]
        public async Task<IActionResult> RedirectToLong(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
            {
                return RedirectToAction("Error", "Error", new { statusCode = StatusCodes.Status404NotFound });
            }
            var longUrlMapTableEntity =
                await _storageService.RetrieveLongUrlMapForShortUrlAsync(shortUrl.ToLowerInvariant());
            if (string.IsNullOrEmpty(longUrlMapTableEntity?.Target))
            {
                return RedirectToAction("Error", "Error", new { statusCode = StatusCodes.Status404NotFound });
            }
            Response.StatusCode = 302;
            return Redirect(longUrlMapTableEntity.Target);
        }

        private async Task<HomeModel> PopulateHomeModel()
        {
            var homeModel = new HomeModel();
            try
            {
                homeModel.Events = await _storageService.RetrieveAllEventsAsync();
            }
            catch (Exception exception)
            {
                _telemetryClient.TrackException(exception);
            }
            return homeModel;
        }
    }
}