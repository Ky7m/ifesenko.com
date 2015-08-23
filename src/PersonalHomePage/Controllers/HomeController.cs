using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using PersonalHomePage.Extensions;
using PersonalHomePage.Models;
using PersonalHomePage.Services;
using PersonalHomePage.Services.HealthService;
using WebMarkupMin.Mvc.ActionFilters;

namespace PersonalHomePage.Controllers
{
    public class HomeController : Controller
    {
        private readonly HealthService _healthService;
        private readonly TelemetryClient _telemetryClient;

        public HomeController()
        {
            _telemetryClient = new TelemetryClient();
            _healthService = new HealthService();
        }

        [CompressContent,
         MinifyHtml,
         OutputCache(CacheProfile = "HomePage")]
        public async Task<ActionResult> Index()
        {
            var homeModel = new HomeModel();

            try
            {
                var sw = Stopwatch.StartNew();
               /* var profile = await _healthService.GetProfileAsync();
                homeModel.LastUpdateTimeUtc = profile?.LastUpdateTime;
                sw.Stop();
                _telemetryClient.TrackEvent("GetProfileAsync", new Dictionary<string,string>(2)
                {
                    {"LastUpdateTimeUtc", homeModel.LastUpdateTimeUtc?.ToString() },
                    {"TimeTaken", sw.Elapsed.ToString() }
                });
                sw.Restart();*/
                var todaysSummary = await _healthService.GetTodaysSummaryAsync();
                var summary = todaysSummary.Summaries.FirstOrDefault();
                homeModel.StepsTaken = summary?.StepsTaken;
                homeModel.CaloriesBurned = summary?.CaloriesBurnedSummary?.TotalCalories;
                sw.Stop();
                _telemetryClient.TrackEvent("GetTodaysSummaryAsync", new Dictionary<string, string>(2)
                {
                    {"StepsTaken", homeModel.StepsTaken?.ToString() },
                    {"CaloriesBurned", homeModel.CaloriesBurned?.ToString() },
                    {"TimeTaken", sw.Elapsed.ToString() }
                });
            }
            catch (Exception exception)
            {
                _telemetryClient.TrackException(exception);
            }

            return View(homeModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SendEmailMessage(EmailMessageModel emailMessage)
        {
            const string internalErrorPleaseTryAgain = "Internal error. Please try again.";
            if (ModelState.IsValid)
            {
                // Attempt to send email
                try
                {
                    await EmailService.SendEmailAsync(emailMessage);
                    return await Task.FromResult(JsonResultBuilder.SuccessResponse("Thank you very much for your email."));
                }
                catch (Exception exception)
                {
                    _telemetryClient.TrackException(exception);
                }
                return await Task.FromResult(JsonResultBuilder.ErrorResponse(internalErrorPleaseTryAgain));
            }
            return await Task.FromResult(JsonResultBuilder.ErrorResponse(internalErrorPleaseTryAgain,
                ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                    .Select(m => m.ErrorMessage).ToArray()));
        }
    }
}