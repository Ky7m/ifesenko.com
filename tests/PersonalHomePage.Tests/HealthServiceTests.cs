using PersonalHomePage.Services.HealthService;
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
            //Assert.NotNull(_healthService.GetDeviceAsync("0A84209A-941A-5D79-A5C1-25E706865EF8").Result);
            Assert.NotNull(_healthService.GetTodaysSummaryAsync().Result);
        }
    }
}
