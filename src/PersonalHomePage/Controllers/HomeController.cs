﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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
        private readonly Lazy<TelemetryClient> _telemetryClient = new Lazy<TelemetryClient>(() => new TelemetryClient());

        private readonly IHealthService _healthService;
        private readonly ICacheService _cacheService;
        private readonly ISettingsService _settingsService;

        public HomeController(IHealthService healthService, ICacheService cacheService, ISettingsService settingsService)
        {
            _healthService = healthService;
            _cacheService = cacheService;
            _settingsService = settingsService;
        }

        [CompressContent,
         MinifyHtml,
         OutputCache(CacheProfile = "HomePage")]
        public async Task<ActionResult> Index()
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
            }
            catch (Exception exception)
            {
                _telemetryClient.Value.TrackException(exception);
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
                    _telemetryClient.Value.TrackException(exception);
                }
                return await Task.FromResult(JsonResultBuilder.ErrorResponse(internalErrorPleaseTryAgain));
            }
            return await Task.FromResult(JsonResultBuilder.ErrorResponse(internalErrorPleaseTryAgain,
                ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                    .Select(m => m.ErrorMessage).ToArray()));
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

        private async Task<Profile> GetProfileAsync()
        {
            const string cacheKey = "HealthService.GetProfileAsync";
            Profile profile = null;

            try
            {
                profile = await _cacheService.GetAsync<Profile>(cacheKey);
            }
            catch (Exception exception)
            {
                _telemetryClient.Value.TrackException(exception);
            }

            if (profile != null)
            {
                return profile;
            }

            profile = await _healthService.GetProfileAsync();
            await _cacheService.StoreAsync(cacheKey, profile, TimeSpan.FromHours(2.0));

            return profile;
        }

        private async Task<Summary> GetTodaysSummaryAsync()
        {
            const string cacheKey = "HealthService.GetTodaysSummaryAsync";
            Summary todaysSummary = null;

            try
            {
                todaysSummary = await _cacheService.GetAsync<Summary>(cacheKey);
            }
            catch (Exception exception)
            {
                _telemetryClient.Value.TrackException(exception);
            }

            if (todaysSummary != null)
            {
                return todaysSummary;
            }

            var summaries = await _healthService.GetTodaysSummaryAsync();
            todaysSummary = summaries.Summaries.FirstOrDefault();
            await _cacheService.StoreAsync(cacheKey, todaysSummary, TimeSpan.FromHours(2.0));

            return todaysSummary;
        }
    }
}