using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;
using Microsoft.ApplicationInsights;
using PersonalHomePage.Extensions;
using PersonalHomePage.Models;
using PersonalHomePage.Services;
using PersonalHomePage.Services.Implementation.HealthService.Model;
using PersonalHomePage.Services.Interfaces;
using WebMarkupMin.Mvc.ActionFilters;

namespace PersonalHomePage.Controllers
{
    public class HomeController : Controller
    {
        #region Fields and Ctor
        
        readonly Lazy<TelemetryClient> _telemetryClient = new Lazy<TelemetryClient>(() => new TelemetryClient());

        readonly IHealthService _healthService;
        readonly ICacheService _cacheService;
        readonly ISettingsService _settingsService;

        public HomeController(IHealthService healthService, ICacheService cacheService, ISettingsService settingsService)
        {
            _healthService = healthService;
            _cacheService = cacheService;
            _settingsService = settingsService;
        }

        #endregion Fields and Ctor
        #region Actions

        [CompressContent,
         MinifyHtml,
         OutputCache(CacheProfile = "HomePage")]
        public async Task<ActionResult> Index()
        {
            var homeModel = await PopulateHomeModel();
            return View(homeModel);
        }

        [HttpPost]
        public async Task<JsonResult> SendEmailMessage(EmailMessageModel emailMessage)
        {
            const string internalErrorPleaseTryAgain = "Internal error. Please try again.";
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Keys.SelectMany(key => ModelState[key].Errors).Select(modelError => modelError.ErrorMessage).ToArray();
                return JsonResultBuilder.ErrorResponse(internalErrorPleaseTryAgain, errors);
            }
            try
            {
                await EmailService.SendEmailAsync(emailMessage);
                return JsonResultBuilder.SuccessResponse("Thank you very much for your email.");
            }
            catch (Exception exception)
            {
                _telemetryClient.Value.TrackException(exception);
            }
            return JsonResultBuilder.ErrorResponse(internalErrorPleaseTryAgain);
        }

        public ActionResult RedirectToLong(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
            {
                return RedirectToAction("NotFound", "Error");
            }
            var longUrlMapTableEntity = _settingsService.RetrieveLongUrlMapForShortUrl(shortUrl.ToLowerInvariant());
            if (string.IsNullOrEmpty(longUrlMapTableEntity?.Target))
            {
                return RedirectToAction("NotFound", "Error");
            }
            Response.StatusCode = 302;
            return Redirect(longUrlMapTableEntity.Target);
        }

        #endregion Actions

        #region Inner

        private async Task<Summary> GetTodaysSummaryAsync()
        {
            return await GetFromCacheOrAddToCacheFromService("HealthService.GetTodaysSummaryAsync", service => service.GetTodaysSummaryAsync());
        }
        private async Task<SleepActivity> GetTodaysSleepActivityAsync()
        {
            return await GetFromCacheOrAddToCacheFromService("HealthService.GetTodaysSleepActivityAsync", service => service.GetTodaysSleepActivityAsync());
        }

        private async Task<HomeModel> PopulateHomeModel()
        {
            var homeModel = new HomeModel();
            try
            {
                var summary = await GetTodaysSummaryAsync();
                homeModel.StepsTaken = summary?.StepsTaken;
                homeModel.CaloriesBurned = summary?.CaloriesBurnedSummary?.TotalCalories;
                homeModel.TotalDistanceOnFoot = summary?.DistanceSummary?.TotalDistanceOnFoot / 100.0 / 1000.0;
                if (homeModel.TotalDistanceOnFoot.HasValue)
                {
                    homeModel.TotalDistanceOnFoot = Math.Round(homeModel.TotalDistanceOnFoot.Value, 2);
                }
                homeModel.AverageHeartRate = summary?.HeartRateSummary?.AverageHeartRate;

                var sleepActivity = await GetTodaysSleepActivityAsync();
                if (!string.IsNullOrEmpty(sleepActivity?.SleepDuration))
                {
                    var sleepDuration = XmlConvert.ToTimeSpan(sleepActivity.SleepDuration);
                    if (sleepDuration.Hours < 4)
                    {
                        sleepDuration += TimeSpan.FromHours(4 - sleepDuration.Hours);
                    }
                    homeModel.SleepDuration = $"{sleepDuration.Hours.ToString()}h {sleepDuration.Minutes.ToString()}m";
                }
                   
                homeModel.SleepEfficiencyPercentage = sleepActivity?.SleepEfficiencyPercentage;
            }
            catch (Exception exception)
            {
                _telemetryClient.Value.TrackException(exception);
            }
            return homeModel;
        }

        private async Task<TReturn> GetFromCacheOrAddToCacheFromService<TReturn>(string key, Func<IHealthService, Task<TReturn>> getFromServiceFunc) 
            where TReturn : class
        {
            var cachedValue = await _cacheService.GetAsync<TReturn>(key);
            if (cachedValue != null)
            {
                return cachedValue;
            }
            cachedValue = await getFromServiceFunc(_healthService);
            await _cacheService.StoreAsync(key, cachedValue, TimeSpan.FromHours(3.0));
            return cachedValue;
        }

        #endregion Inner
    }
}