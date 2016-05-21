//using System.Collections.Generic;
//using ifesenko.com.Infrastructure.Services.Implementation;
//using ifesenko.com.Infrastructure.Services.Implementation.CloudStorageService;
//using ifesenko.com.Infrastructure.Services.Implementation.HealthService;
//using ifesenko.com.Infrastructure.Settings;
//using Microsoft.Extensions.OptionsModel;
//using Xunit;

//namespace ifesenko.com.tests
//{
//    public sealed class HealthServiceIntegrationTests
//    {
//        private readonly HealthService _healthService;
//        public HealthServiceIntegrationTests()
//        {
//            var appSettings = new OptionsManager<AppSettings>(new List<IConfigureOptions<AppSettings>>
//            {
//                new ConfigureOptions<AppSettings>(settings =>
//                {
//                    settings.StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=ifesenko;AccountKey=eaf2ozvWqyIgqak/HLdqSbHYefCoyPRN41o+tIX6a3mzIN9Z2bu9fdbX19wXikVuKG8YZhDtY2aOJSJL+/s/uw==";

//                })
//            });

//            _healthService = new HealthService(new SettingsService(new CloudStorageService(appSettings)));
//        }


//        [Fact]
//        public void CheckThatTodaysSummaryIsNotNull()
//        {
//            var summariesResponse = _healthService.GetTodaysSummaryAsync().Result;
//            Assert.NotNull(summariesResponse);
//        }

//        [Fact]
//        public void CheckThatSleepActivityIsNotNull()
//        {
//            var sleepActivity = _healthService.GetTodaysSleepActivityAsync().Result;
//            Assert.NotNull(sleepActivity);
//        }
//    }
//}
