﻿using System;
using System.Threading.Tasks;
using PersonalHomePage.Services.Implementation;
using PersonalHomePage.Services.Implementation.CloudStorageService;
using PersonalHomePage.Services.Implementation.HealthService;
using PersonalHomePage.Services.Implementation.HealthService.Model;
using Xunit;

namespace PersonalHomePage.Tests
{
    public class RedisCacheServiceIntegrationTests
    {
        readonly HealthService _healthService;
        readonly RedisCacheService _redisCacheService;
        public RedisCacheServiceIntegrationTests()
        {
            _healthService = new HealthService(new SettingsService(new CloudStorageService()));
            _redisCacheService = new RedisCacheService();
        }

        [Fact]
        public async Task CheckThatTodaysSummaryIsNotNull()
        {
            var cacheKey = "Test.HealthService.GetTodaysSummaryAsync";
            await _redisCacheService.DeleteAsync(cacheKey);
            var summary = await _redisCacheService.GetAsync<Summary>(cacheKey);
            if (summary == null)
            {
                summary = await _healthService.GetTodaysSummaryAsync();
                await _redisCacheService.StoreAsync(cacheKey, summary, TimeSpan.FromSeconds(60));
            }
            var expected = await _redisCacheService.GetAsync<Summary>(cacheKey);
            Assert.Equal(summary.StepsTaken, expected.StepsTaken);
        }

        [Fact]
        public async Task CheckThatSleepActivityIsNotNull()
        {
            var cacheKey = "Test.HealthService.CheckThatSleepActivityIsNotNull";
            await _redisCacheService.DeleteAsync(cacheKey);
            var sleepActivity = await _redisCacheService.GetAsync<SleepActivity>(cacheKey);
            if (sleepActivity == null)
            {
                sleepActivity = await _healthService.GetTodaysSleepActivityAsync();
                await _redisCacheService.StoreAsync(cacheKey, sleepActivity, TimeSpan.FromSeconds(60));
            }
            var expected = await _redisCacheService.GetAsync<SleepActivity>(cacheKey);
            Assert.Equal(sleepActivity.SleepEfficiencyPercentage, expected.SleepEfficiencyPercentage);
        }
    }
}