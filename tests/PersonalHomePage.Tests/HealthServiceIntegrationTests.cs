using PersonalHomePage.Services.HealthService;
using PersonalHomePage.Services.HealthService.Model;
using PersonalHomePage.Services.HealthService.Model.Responses;
using Xunit;

namespace PersonalHomePage.Tests
{
    public class HealthServiceIntegrationTests
    {
        readonly HealthService _healthService;
        public HealthServiceIntegrationTests()
        {
            _healthService = new HealthService();
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
