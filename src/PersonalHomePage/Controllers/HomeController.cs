﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.ApplicationInsights;
using PersonalHomePage.Extensions;
using PersonalHomePage.Models;
using PersonalHomePage.Services;
using PersonalHomePage.Services.HealthService;
using PersonalHomePage.Services.HealthService.Model;
using WebMarkupMin.Mvc.ActionFilters;

namespace PersonalHomePage.Controllers
{
    public class HomeController : Controller
    {
        private readonly Lazy<TelemetryClient> _telemetryClient = new Lazy<TelemetryClient>(() => new TelemetryClient());

        private readonly Lazy<HealthService> _healthService = new Lazy<HealthService>(() => new HealthService());
        private readonly Lazy<RedisCacheService> _redisCacheService = new Lazy<RedisCacheService>(() => new RedisCacheService());

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

        private async Task<Profile> GetProfileAsync()
        {
            var cacheKey = "HealthService.GetProfileAsync";
            Profile profile = null;

            try
            {
                profile = _redisCacheService.Value.Get<Profile>(cacheKey);
            }
            catch (Exception exception)
            {
                _telemetryClient.Value.TrackException(exception);
            }

            if (profile != null)
            {
                return profile;
            }

            profile = await _healthService.Value.GetProfileAsync();
            _redisCacheService.Value.Store(cacheKey, profile, TimeSpan.FromHours(2.0));

            return profile;
        }

        private async Task<Summary> GetTodaysSummaryAsync()
        {
            var cacheKey = "HealthService.GetTodaysSummaryAsync";
            Summary todaysSummary = null;

            try
            {
                todaysSummary = _redisCacheService.Value.Get<Summary>(cacheKey);
            }
            catch (Exception exception)
            {
                _telemetryClient.Value.TrackException(exception);
            }

            if (todaysSummary != null)
            {
                return todaysSummary;
            }

            var summaries = await _healthService.Value.GetTodaysSummaryAsync();
            todaysSummary = summaries.Summaries.FirstOrDefault();
            _redisCacheService.Value.Store(cacheKey, todaysSummary, TimeSpan.FromHours(2.0));

            return todaysSummary;
        }
    }
}