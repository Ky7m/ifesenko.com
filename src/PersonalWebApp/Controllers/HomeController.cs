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

            // var sb = new StringBuilder();
            // foreach (var eventModel in result.Events)
            // {
            //     foreach (var item in eventModel.Items)
            //     {
            //         var recording = string.Empty;
            //         var ppt = string.Empty;
            //         foreach (var pair in item.Collateral)
            //         {
            //             if (string.Equals(pair.Value, CommonStrings.VideoRus))
            //             {
            //                 recording = pair.Key;
            //             }
            //             if (string.Equals(pair.Value, CommonStrings.Powerpoint))
            //             {
            //                 ppt = pair.Key;
            //             }
            //         }
            //         sb.AppendLine($"{eventModel.Date:d},\"{eventModel.Title}\",{eventModel.Link},\"{item.Description}\",\"{ppt}\",\"{recording}\"");
            //     }
            // }
            // System.IO.File.WriteAllText("events.csv", sb.ToString());
            var homeModel = new HomeModel {Events = result.Events, IsItAllEvents = result.IsItAllEvents};
            return View(homeModel);
        }
    }
}