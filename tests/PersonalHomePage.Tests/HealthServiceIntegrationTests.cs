using PersonalHomePage.Services.HealthService;
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
            Assert.NotNull(_healthService.GetProfileAsync().Result);
        }

        [Fact]
        public void CheckThatTodaysSummaryIsNotNull()
        {
            Assert.NotNull(_healthService.GetTodaysSummaryAsync().Result);
        }
    }
}
