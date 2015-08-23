﻿using PersonalHomePage.Services.HealthService;
using Xunit;

namespace PersonalHomePage.Tests
{
    public class HealthServiceTests
    {
        readonly HealthService _healthService;
        public HealthServiceTests()
        {
            _healthService = new HealthService();
        }

        //[Fact]
        public void CheckProfile()
        {
            Assert.NotNull(_healthService.GetTodaysSummaryAsync().Result);
        }
    }
}
