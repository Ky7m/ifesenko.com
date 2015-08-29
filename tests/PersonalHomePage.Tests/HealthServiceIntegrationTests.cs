using PersonalHomePage.Services;
using PersonalHomePage.Services.CloudStorageService;
using PersonalHomePage.Services.HealthService;
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
