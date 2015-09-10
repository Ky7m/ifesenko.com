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
            const string cacheKey = "Test.HealthService.GetProfileAsync";
            await _redisCacheService.DeleteAsync(cacheKey);
            var profile = await _redisCacheService.GetAsync<Profile>(cacheKey);
            if (profile == null)
            {
                profile = await _healthService.GetProfileAsync();
                await _redisCacheService.StoreAsync(cacheKey, profile, TimeSpan.FromSeconds(30));
            }
            var expected = _redisCacheService.GetAsync<Profile>(cacheKey);
            Assert.NotNull(expected);
        }

        [Fact]
        public async Task CheckThatTodaysSummaryIsNotNull()
        {
            var cacheKey = "Test.HealthService.GetTodaysSummaryAsync";
            await _redisCacheService.DeleteAsync(cacheKey);
            var summary = await _redisCacheService.GetAsync<Summary>(cacheKey);
            if (summary == null)
            {
                var summaries = await _healthService.GetTodaysSummaryAsync();
                summary = summaries.Summaries.FirstOrDefault();
                await _redisCacheService.StoreAsync(cacheKey, summary, TimeSpan.FromSeconds(60));
            }
            var expected = _redisCacheService.GetAsync<Summary>(cacheKey);
            Assert.NotNull(expected);
        }

    }
}
