using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IfesenkoDotCom.Services.Implementation;
using IfesenkoDotCom.Services.Implementation.CloudStorageService;
using IfesenkoDotCom.Services.Implementation.HealthService;
using IfesenkoDotCom.Services.Implementation.HealthService.Model;
using IfesenkoDotCom.Settings;
using Microsoft.Extensions.OptionsModel;
using Xunit;

namespace ifesenko.com.tests
{
    public sealed class RedisCacheServiceIntegrationTests
    {
        private readonly HealthService _healthService;
        private readonly RedisCacheService _redisCacheService;
        public RedisCacheServiceIntegrationTests()
        {
            var appSettings = new OptionsManager<AppSettings>(new List<IConfigureOptions<AppSettings>>
            {
                new ConfigureOptions<AppSettings>(settings =>
                {
                    settings.RedisCacheConnectionString = "ifesenko.redis.cache.windows.net:6380,ssl=true,abortConnect=false,password=bUnADG6dGGP9kAYjcgMfbNgexGGk9ChIBPrPhiSciw0=";
                    settings.StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=ifesenko;AccountKey=eaf2ozvWqyIgqak/HLdqSbHYefCoyPRN41o+tIX6a3mzIN9Z2bu9fdbX19wXikVuKG8YZhDtY2aOJSJL+/s/uw==";

                })
            });
            _healthService = new HealthService(new SettingsService(new CloudStorageService(appSettings)));
            _redisCacheService = new RedisCacheService(appSettings);
        }

        [Fact]
        public async Task CheckThatTodaysSummaryIsNotNull()
        {
            var cacheKey = "Test.HealthService.GetTodaysSummaryAsync";
            await _redisCacheService.DeleteAsync(cacheKey);
            var summary = await _redisCacheService.GetAsync<Summary>(cacheKey);
            if (summary == null)
            {
                summary = await _healthService.GetTodaysSummaryAsync();
                await _redisCacheService.StoreAsync(cacheKey, summary, TimeSpan.FromSeconds(60));
            }
            var expected = await _redisCacheService.GetAsync<Summary>(cacheKey);
            Assert.Equal(summary.StepsTaken, expected.StepsTaken);
        }

        [Fact]
        public async Task CheckThatSleepActivityIsNotNull()
        {
            var cacheKey = "Test.HealthService.CheckThatSleepActivityIsNotNull";
            await _redisCacheService.DeleteAsync(cacheKey);
            var sleepActivity = await _redisCacheService.GetAsync<SleepActivity>(cacheKey);
            if (sleepActivity == null)
            {
                sleepActivity = await _healthService.GetTodaysSleepActivityAsync();
                await _redisCacheService.StoreAsync(cacheKey, sleepActivity, TimeSpan.FromSeconds(60));
            }
            var expected = await _redisCacheService.GetAsync<SleepActivity>(cacheKey);
            Assert.Equal(sleepActivity.SleepEfficiencyPercentage, expected.SleepEfficiencyPercentage);
        }
    }
}
