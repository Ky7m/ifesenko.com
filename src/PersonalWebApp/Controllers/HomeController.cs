using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using PersonalWebApp.Infrastructure.Services.Implementation.HealthService.Model;
using PersonalWebApp.Infrastructure.Services.Interfaces;
using PersonalWebApp.Models;

namespace PersonalWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHealthService _healthService;
        private readonly ICacheService _cacheService;
        private readonly IStorageService _storageService;
        private readonly TelemetryClient _telemetryClient;

        public HomeController(IHealthService healthService,
            ICacheService cacheService,
            IStorageService storageService,
            TelemetryClient telemetryClient)
        {
            _healthService = healthService;
            _cacheService = cacheService;
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
                var statsModel = new StatsModel();
                var summary = await GetTodaysSummaryAsync();
                if (summary != null)
                {
                    statsModel.StepsTaken = summary.StepsTaken;
                    statsModel.CaloriesBurned = summary.CaloriesBurnedSummary.TotalCalories;
                    statsModel.TotalDistanceOnFoot = summary.DistanceSummary.TotalDistanceOnFoot / 100.0 / 1000.0;
                    statsModel.AverageHeartRate = summary.HeartRateSummary.AverageHeartRate;
                }
                if (statsModel.TotalDistanceOnFoot.HasValue)
                {
                    statsModel.TotalDistanceOnFoot = Math.Round(statsModel.TotalDistanceOnFoot.Value, 2);
                }

                var sleepActivity = await GetTodaysSleepActivityAsync();
                if (sleepActivity != null)
                {
                    if (!string.IsNullOrEmpty(sleepActivity.SleepDuration))
                    {
                        var sleepDuration = XmlConvert.ToTimeSpan(sleepActivity.SleepDuration);
                        if (sleepDuration.Hours < 4)
                        {
                            sleepDuration += TimeSpan.FromHours(4 - sleepDuration.Hours);
                        }
                        statsModel.SleepDuration = $"{sleepDuration.Hours}h {sleepDuration.Minutes}m";
                    }

                    statsModel.SleepEfficiencyPercentage = sleepActivity?.SleepEfficiencyPercentage;
                }
                homeModel.Stats = statsModel;
                homeModel.Events = await _storageService.RetrieveAllEventsAsync();
            }
            catch (Exception exception)
            {
                _telemetryClient.TrackException(exception);
            }
            return homeModel;
        }

        private async Task<Summary> GetTodaysSummaryAsync()
        {
            return
                await
                    GetFromServiceUsingCache(_healthService, service => service.GetTodaysSummaryAsync(),
                        TimeSpan.FromHours(3.0));
        }

        private async Task<SleepActivity> GetTodaysSleepActivityAsync()
        {
            return
                await
                    GetFromServiceUsingCache(_healthService, service => service.GetTodaysSleepActivityAsync(),
                        TimeSpan.FromHours(4.0));
        }

        private async Task<TReturn> GetFromServiceUsingCache<TService, TReturn>(TService service,
            Func<TService, Task<TReturn>> getFromServiceFunc,
            TimeSpan? expiryTime = null,
            [CallerMemberName] string memberName = "")
            where TReturn : class
        {
            var version = PlatformServices.Default.Application.ApplicationVersion;
            var key = $"{nameof(HomeController)}.{memberName}.{version}";
            TReturn cachedValue = null;
            try
            {
                cachedValue = await _cacheService.GetAsync<TReturn>(key);
                if (cachedValue != null)
                {
                    return cachedValue;
                }
                cachedValue = await getFromServiceFunc(service);
                await _cacheService.StoreAsync(key, cachedValue, expiryTime ?? TimeSpan.FromHours(3.0));
            }
            catch (Exception exception)
            {
                _telemetryClient.TrackException(exception);
            }
            return cachedValue;
        }
    }
}