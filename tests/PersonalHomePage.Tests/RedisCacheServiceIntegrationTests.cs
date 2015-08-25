using System;
using System.Threading.Tasks;
using PersonalHomePage.Services;
using PersonalHomePage.Services.HealthService;
using PersonalHomePage.Services.HealthService.Model;
using Xunit;

namespace PersonalHomePage.Tests
{
    public class RedisCacheServiceIntegrationTests
    {
        readonly HealthService _healthService;
        readonly RedisCacheService _redisCacheService;
        public RedisCacheServiceIntegrationTests()
        {
            _healthService = new HealthService();
            _redisCacheService = new RedisCacheService();
        }

        [Fact]
        public async Task CheckThatProfileIsNotNull()
        {
            var profile = await _redisCacheService.GetAsync<Profile>("HealthService.GetProfileAsync");
            if (profile == null)
            {
                profile = await _healthService.GetProfileAsync();
                await _redisCacheService.StoreAsync("HealthService.GetProfileAsync", profile, TimeSpan.FromMinutes(1.0));
            }
            var expected = await _redisCacheService.GetAsync<Profile>("HealthService.GetProfileAsync");
            Assert.NotNull(expected);
        }

    }
}
