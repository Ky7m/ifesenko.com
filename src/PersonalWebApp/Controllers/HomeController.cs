using Microsoft.AspNetCore.Mvc;
using PersonalWebApp.Models;
using PersonalWebApp.Services;

namespace PersonalWebApp.Controllers;

public class HomeController(IStorageService storageService) : Controller
{
    [Route("/")]
    [ResponseCache(CacheProfileName = "HomePage")]
    public IActionResult Index(string period)
    {
        var result = storageService.RetrieveEventsForPeriod(period);
        var homeModel = new HomeModel {Events = result.Events, IsItAllEvents = result.IsItAllEvents};
        return View(homeModel);
    }
}