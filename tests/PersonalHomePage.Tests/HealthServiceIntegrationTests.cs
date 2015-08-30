﻿using PersonalHomePage.Services.Implementation;
using PersonalHomePage.Services.Implementation.CloudStorageService;
using PersonalHomePage.Services.Implementation.HealthService;
using Xunit;

namespace PersonalHomePage.Tests
{
    public class HealthServiceIntegrationTests
    {
        readonly HealthService _healthService;
        public HealthServiceIntegrationTests()
        {
            _healthService = new HealthService(new SettingsService(new CloudStorageService()));
        }

        [Fact]
        public void CheckThatProfileIsNotNull()
        {
            var profile = _healthService.GetProfileAsync().Result;
            Assert.NotNull(profile);
        }

        [Fact]
        public void CheckThatTodaysSummaryIsNotNull()
        {
            var summariesResponse = _healthService.GetTodaysSummaryAsync().Result;
            Assert.NotNull(summariesResponse);
        }
    }
}
