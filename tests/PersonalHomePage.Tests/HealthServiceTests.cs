using PersonalHomePage.Services;
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
        public void CheckAuthorizationRequestUri()
        {
            Assert.Equal(_healthService.GetAuthorizationRequestUri().ToString(), 
                "https://login.live.com/oauth20_authorize.srf?redirect_uri=https://login.live.com/oauth20_desktop.srf&client_id=0000000048160472&client_secret=1F7MndfGzAh1uMMMAH5aQ6oSw0H8bXD4&scope=mshealth.ReadProfile mshealth.ReadActivityHistory mshealth.ReadDevices mshealth.ReadActivityLocation offline_access&response_type=code");
        }

        //[Fact]
        public void CheckOAuthTokenRequestRequestUri()
        {
            Assert.Equal(_healthService.CreateOAuthTokenRequestUri("code",false).ToString(),
                "https://login.live.com/oauth20_token.srf?redirect_uri=https://login.live.com/oauth20_desktop.srf&client_id=0000000048160472&client_secret=1F7MndfGzAh1uMMMAH5aQ6oSw0H8bXD4&code=code&grant_type=authorization_code");
        }

       // [Fact]
        public void CheckProfile()
        {
            Assert.NotEmpty(_healthService.GetActivitySummary("Sleep").Result);
        }
    }
}
