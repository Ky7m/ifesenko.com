using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml;
using IfesenkoDotCom.Models;
using IfesenkoDotCom.Services.Implementation.HealthService.Model;
using IfesenkoDotCom.Services.Interfaces;
using Microsoft.AspNet.Mvc;

namespace IfesenkoDotCom.Controllers
{
    public class HomeController : Controller
    {
        #region Fields and Ctor

        private readonly IHealthService _healthService;
        private readonly ICacheService _cacheService;
        private readonly IStorageService _storageService;

        public HomeController(IHealthService healthService,
            ICacheService cacheService,
            IStorageService storageService)
        {
            _healthService = healthService;
            _cacheService = cacheService;
            _storageService = storageService;
        }

        #endregion Fields and Ctor

        #region Actions

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var homeModel = await PopulateHomeModel();
            return View(homeModel);
        }

        //[HttpPost]
        //public async Task<JsonResult> SendEmailMessage(EmailMessageModel emailMessage)
        //{
        //    const string internalErrorPleaseTryAgain = "Internal error. Please try again.";
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Keys.SelectMany(key => ModelState[key].Errors).Select(modelError => modelError.ErrorMessage).ToArray();
        //        return JsonResultBuilder.ErrorResponse(internalErrorPleaseTryAgain, errors);
        //    }
        //    try
        //    {
        //        await EmailService.SendEmailAsync(emailMessage);
        //        return JsonResultBuilder.SuccessResponse("Thank you very much for your email.");
        //    }
        //    catch (Exception exception)
        //    {
        //        _telemetryClient.Value.TrackException(exception);
        //    }
        //    return JsonResultBuilder.ErrorResponse(internalErrorPleaseTryAgain);
        //}

        //public async Task<ActionResult> RedirectToLong(string shortUrl)
        //{
        //    if (string.IsNullOrEmpty(shortUrl))
        //    {
        //        return RedirectToAction("NotFound", "Error");
        //    }
        //    var longUrlMapTableEntity = await _storageService.RetrieveLongUrlMapForShortUrlAsync(shortUrl.ToLowerInvariant());
        //    if (string.IsNullOrEmpty(longUrlMapTableEntity?.Target))
        //    {
        //        return RedirectToAction("NotFound", "Error");
        //    }
        //    Response.StatusCode = 302;
        //    return Redirect(longUrlMapTableEntity.Target);
        //}

        #endregion Actions

        #region Inner

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
                var ex = exception;
                // _telemetryClient.Value.TrackException(exception);
            }
            return homeModel;
        }

        private async Task<Summary> GetTodaysSummaryAsync()
        {
            return await GetFromCacheOrAddToCacheFromService(_healthService, service => service.GetTodaysSummaryAsync(), TimeSpan.FromHours(1.0));
        }
        private async Task<SleepActivity> GetTodaysSleepActivityAsync()
        {
            return await GetFromCacheOrAddToCacheFromService(_healthService, service => service.GetTodaysSleepActivityAsync(), TimeSpan.FromHours(8.0));
        }

        private async Task<TReturn> GetFromCacheOrAddToCacheFromService<TService, TReturn>(TService service, Func<TService, Task<TReturn>> getFromServiceFunc, TimeSpan? expiryTime = null, [CallerMemberName] string memberName = "")
            where TReturn : class
        {
            var key = $"{nameof(HomeController)}.{memberName}";
            var cachedValue = await _cacheService.GetAsync<TReturn>(key);
            if (cachedValue != null)
            {
                return cachedValue;
            }
            cachedValue = await getFromServiceFunc(service);
            await _cacheService.StoreAsync(key, cachedValue, expiryTime ?? TimeSpan.FromHours(3.0));
            return cachedValue;
        }

        #endregion Inner
    }
}