using System;
using System.Linq;
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
        public async Task CheckThatProfileIsNotNull()
        {
            var cacheKey = "Test.HealthService.GetProfileAsync";
            _redisCacheService.Delete(cacheKey);
            var profile = _redisCacheService.Get<Profile>(cacheKey);
            if (profile == null)
            {
                profile = await _healthService.GetProfileAsync();
                _redisCacheService.Store(cacheKey, profile, TimeSpan.FromSeconds(30));
            }
            var expected = _redisCacheService.Get<Profile>(cacheKey);
            Assert.NotNull(expected);
        }

        [Fact]
        public async Task CheckThatTodaysSummaryIsNotNull()
        {
            var cacheKey = "Test.HealthService.GetTodaysSummaryAsync";
            _redisCacheService.Delete(cacheKey);
            var summary = _redisCacheService.Get<Summary>(cacheKey);
            if (summary == null)
            {
                var summaries = await _healthService.GetTodaysSummaryAsync();
                summary = summaries.Summaries.FirstOrDefault();
                _redisCacheService.Store(cacheKey, summary, TimeSpan.FromSeconds(60));
            }
            var expected = _redisCacheService.Get<Summary>(cacheKey);
            Assert.NotNull(expected);
        }

    }
}
