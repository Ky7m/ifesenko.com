using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml;
using ifesenko.com.Infrastructure.Services.Implementation.HealthService.Model;
using ifesenko.com.Infrastructure.Services.Interfaces;
using ifesenko.com.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace ifesenko.com.Controllers
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
                return RedirectToAction("Error","Error",new { statusCode = StatusCodes.Status404NotFound });
            }
            var longUrlMapTableEntity = await _storageService.RetrieveLongUrlMapForShortUrlAsync(shortUrl.ToLowerInvariant());
            if (string.IsNullOrEmpty(longUrlMapTableEntity?.Target))
            {
                return RedirectToAction("Error", "Error", StatusCodes.Status404NotFound);
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
                statsModel.StepsTaken = summary?.StepsTaken;
                statsModel.CaloriesBurned = summary?.CaloriesBurnedSummary.TotalCalories;
                statsModel.TotalDistanceOnFoot = summary?.DistanceSummary.TotalDistanceOnFoot / 100.0 / 1000.0;
                if (statsModel.TotalDistanceOnFoot.HasValue)
                {
                    statsModel.TotalDistanceOnFoot = Math.Round(statsModel.TotalDistanceOnFoot.Value, 2);
                }
                statsModel.AverageHeartRate = summary?.HeartRateSummary.AverageHeartRate;

                var sleepActivity = await GetTodaysSleepActivityAsync();
                if (!string.IsNullOrEmpty(sleepActivity?.SleepDuration))
                {
                    var sleepDuration = XmlConvert.ToTimeSpan(sleepActivity.SleepDuration);
                    if (sleepDuration.Hours < 4)
                    {
                        sleepDuration += TimeSpan.FromHours(4 - sleepDuration.Hours);
                    }
                    statsModel.SleepDuration = $"{sleepDuration.Hours}h {sleepDuration.Minutes}m";
                }

                statsModel.SleepEfficiencyPercentage = sleepActivity?.SleepEfficiencyPercentage;

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
            return await GetFromCacheOrAddToCacheFromService(_healthService, service => service.GetTodaysSummaryAsync(), TimeSpan.FromHours(3.0));
        }
        private async Task<SleepActivity> GetTodaysSleepActivityAsync()
        {
            return await GetFromCacheOrAddToCacheFromService(_healthService, service => service.GetTodaysSleepActivityAsync(), TimeSpan.FromHours(4.0));
        }

        private async Task<TReturn> GetFromCacheOrAddToCacheFromService<TService, TReturn>(TService service, Func<TService, Task<TReturn>> getFromServiceFunc, TimeSpan? expiryTime = null, [CallerMemberName] string memberName = "")
            where TReturn : class
        {
            var key = $"{nameof(HomeController)}.{memberName}.ASPNET";
            var cachedValue = await _cacheService.GetAsync<TReturn>(key);
            if (cachedValue != null)
            {
                return cachedValue;
            }
            cachedValue = await getFromServiceFunc(service);
            await _cacheService.StoreAsync(key, cachedValue, expiryTime ?? TimeSpan.FromHours(3.0));
            return cachedValue;
        }
    }
}