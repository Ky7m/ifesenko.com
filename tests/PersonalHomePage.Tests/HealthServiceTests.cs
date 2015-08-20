using PersonalHomePage.Services.HealthService;
using Xunit;

namespace PersonalHomePage.Tests
{
    public class HealthServiceTests
    {
        readonly HealthService _healthServiceOld;
        public HealthServiceTests()
        {
            _healthServiceOld = new HealthService();
        }

        //[Fact]
        public void CheckProfile()
        {
            Assert.NotNull(_healthServiceOld.GetTodaysSummaryAsync().Result);
        }
    }
}
