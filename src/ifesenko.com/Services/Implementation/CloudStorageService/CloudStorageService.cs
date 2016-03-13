using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IfesenkoDotCom.Infrastructure.Extensions;
using IfesenkoDotCom.Models;
using IfesenkoDotCom.Services.Implementation.CloudStorageService.Model;
using IfesenkoDotCom.Services.Interfaces;
using IfesenkoDotCom.Settings;
using Microsoft.Extensions.OptionsModel;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace IfesenkoDotCom.Services.Implementation.CloudStorageService
{
    public sealed class CloudStorageService : IStorageService
    {
        private readonly AppSettings _appSettings;
        private readonly Lazy<CloudStorageAccount> _cloudStorageAccount;

        public CloudStorageService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _cloudStorageAccount = new Lazy<CloudStorageAccount>(() => CloudStorageAccount.Parse(_appSettings.StorageConnectionString));
        }

        public async Task<SettingTableEntity[]> RetrieveAllSettingsForServiceAsync(string serviceName)
        {
            var tableQuery = new TableQuery<SettingTableEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, serviceName));

            return await ExecuteQueryAsync("Settings", tableQuery);
        }

        public async Task ReplaceSettingValueForServiceAsync(SettingTableEntity updateSettingTableEntity)
        {
            var client = _cloudStorageAccount.Value.CreateCloudTableClient();
            var table = client.GetTableReference("Settings");

            var retrieveOperation =
                TableOperation.Retrieve<SettingTableEntity>(updateSettingTableEntity.PartitionKey,
                    updateSettingTableEntity.RowKey);

            var newValue = updateSettingTableEntity.Value;
            /*
                        Policy
                          .Handle<StorageException>(ex => ex.RequestInformation.HttpStatusCode == (int)HttpStatusCode.PreconditionFailed)
                          .Retry(3)
                          .Execute(() =>
                          {*/
            var retrievedResult = await table.ExecuteAsync(retrieveOperation);

            var updateEntity = (SettingTableEntity)retrievedResult.Result;
            if (updateEntity == null)
            {
                return;
            }

            updateEntity.Value = newValue;
            var updateOperation = TableOperation.Replace(updateEntity);
            await table.ExecuteAsync(updateOperation);
            /*});*/
        }

        public async Task<ShortToLongUrlMapTableEntity> RetrieveLongUrlMapForShortUrlAsync(string shortUrl)
        {
            var client = _cloudStorageAccount.Value.CreateCloudTableClient();
            var table = client.GetTableReference("ShortToLongUrlsMap");

            var query =
                new TableQuery<ShortToLongUrlMapTableEntity>().Where(TableQuery.GenerateFilterCondition("RowKey",
                    QueryComparisons.Equal, shortUrl));

            var shortToLongUrlMapTableEntity = table.ExecuteQuery(query).ToArray().FirstOrDefault();
            if (shortToLongUrlMapTableEntity == null)
            {
                return null;
            }
            shortToLongUrlMapTableEntity.Stats++;
            var replaceOperation = TableOperation.Replace(shortToLongUrlMapTableEntity);
            await table.ExecuteAsync(replaceOperation);

            return shortToLongUrlMapTableEntity;
        }

        public async Task<EventModel[]> RetrieveAllEventsAsync()
        {
            var events = PopulateEvents();
            return await Task.FromResult(events);
        }

        private async Task<T[]> ExecuteQueryAsync<T>(string tableName, TableQuery<T> tableQuery = null) where T : ITableEntity, new()
        {
            var result = new List<T>();
            var client = _cloudStorageAccount.Value.CreateCloudTableClient();
            var table = client.GetTableReference(tableName);
            if (tableQuery == null)
            {
                tableQuery = new TableQuery<T>();
            }
            TableContinuationToken continuationToken = null;
            do
            {
                var tableQueryResult = await table.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);

                continuationToken = tableQueryResult.ContinuationToken;
                result.AddRange(tableQueryResult.Results);

                // Loop until a null continuation token is received, indicating the end of the table.
            } while (continuationToken != null);

            return result.ToArray();
        }

        private static EventModel[] PopulateEvents()
        {
            return new[]
            {
                new EventModel
                {
                    Title = CommonStrings.DotNetCommunitySoftserve,
                    Description = "Exception Handling - Advanced Tips & Tricks",
                    Collateral = new Dictionary<string, string>(3)
                    {
                        ["https://www.youtube.com/watch?v=_Gw_9KeJlbg"] = CommonStrings.CollateralVideoRus,
                        ["https://doc.co/NwAbvv"] = CommonStrings.CollateralPowerpoint,
                        ["https://ifesenko.blob.core.windows.net/assets/ExceptionHandlingAdvancedTipsAndTricks.zip"] =
                            CommonStrings.CollateralDemoCode
                    },
                    Location = CommonStrings.LocationWebcast,
                    Date = new DateTime(2016, 2, 24)
                },
                new EventModel
                {
                    Title = "Web UI Community - SoftServe",
                    Description = "Web Development in Future",
                    Collateral = new Dictionary<string, string>(3)
                    {
                        ["https://www.youtube.com/watch?v=Om_NPxSuEf8"] = CommonStrings.CollateralVideoRus,
                        ["https://sway.com/LxEYWidCXvki-OHN"] = CommonStrings.CollateralSway
                    },
                    Location = CommonStrings.LocationWebcast,
                    Date = new DateTime(2016, 2, 18)
                },
                new EventModel
                {
                    Title = ".Net Morning@Lohika",
                    Link = "https://www.facebook.com/events/688982824575438/",
                    Description = "Effective Memory Management - Memory Hygiene",
                    Collateral = new Dictionary<string, string>(3)
                    {
                        ["https://www.youtube.com/watch?v=vUX2wFciHrs"] = CommonStrings.CollateralVideoRus,
                        ["https://doc.co/LEvVSM"] = CommonStrings.CollateralPowerpoint,
                        ["https://ifesenko.blob.core.windows.net/assets/EffectiveMemoryManagement.zip"] =
                            CommonStrings.CollateralDemoCode
                    },
                    Location = "Lviv (Ukraine)",
                    Date = new DateTime(2016, 2, 13)
                },
                new EventModel
                {
                    Title = CommonStrings.DotNetCommunitySoftserve,
                    Description = "How C# 6.0 Simplifies Your Code",
                    Collateral = new Dictionary<string, string>(1)
                    {
                        ["https://www.youtube.com/watch?v=gUfrK1rGWB0"] = CommonStrings.CollateralVideoRus
                    },
                    Location = CommonStrings.LocationWebcast,
                    Date = new DateTime(2016, 2, 10)
                },
                new EventModel
                {
                    Title = "Enable Application Innovation Immersion",
                    Link = "https://www.microsoftevents.com/profile/form/index.cfm?PKformID=0x40756dbc3",
                    Description =
                        "The Enable Application Innovation Immersion is a mix of discussion, application architecture design, white boarding and hands-on labs. Your team will design the future of business with new functionalities that augment existing business applications and drive development and execution of new initiatives.",
                    Location = "Dallas (USA)",
                    Date = new DateTime(2016, 1, 26)
                },
                new EventModel
                {
                    Title = "Tech#Skills_Day 1.2.",
                    Link = "https://www.facebook.com/events/1650103461936579/1654170548196537/",
                    Description = "Effective Memory Management: Memory Hygiene",
                    Collateral = new Dictionary<string, string>(3)
                    {
                        ["https://doc.co/LEvVSM"] = CommonStrings.CollateralPowerpoint
                    },
                    Location = "Lviv (Ukraine)",
                    Date = new DateTime(2015, 12, 21)
                },
                new EventModel
                {
                    Title = CommonStrings.DotNetCommunitySoftserve,
                    Description = "C# GTD : Get Things Done in Bloody Enterprise",
                    Collateral = new Dictionary<string, string>(3)
                    {
                        ["https://www.youtube.com/watch?v=hOjNpHeMvvw"] = CommonStrings.CollateralVideoRus,
                        ["https://doc.co/2EFqPy"] = CommonStrings.CollateralPowerpoint
                    },
                    Location = CommonStrings.LocationWebcast,
                    Date = new DateTime(2015, 12, 16)
                },
                new EventModel
                {
                    Title = CommonStrings.DotNetCommunitySoftserve,
                    Description = "C# : Hack yourself",
                    Collateral = new Dictionary<string, string>(3)
                    {
                        ["https://www.youtube.com/watch?v=WHwy0vDd8Tg"] = CommonStrings.CollateralVideoRus,
                        ["https://doc.co/13tWp4"] = CommonStrings.CollateralPowerpoint
                    },
                    Location = CommonStrings.LocationWebcast,
                    Date = new DateTime(2015, 11, 19)
                },
                new EventModel
                {
                    Title = "IT Weekend Kharkiv",
                    Link = "https://itweekend.ua/en/announcements/itw-kh-nov/",
                    Description = "Web Development in Future",
                    Collateral = new Dictionary<string, string>(3)
                    {
                        ["https://sway.com/LxEYWidCXvki-OHN"] = CommonStrings.CollateralSway
                    },
                    Location = "Kharkiv (Ukraine)",
                    Date = new DateTime(2015, 11, 14)
                },
                new EventModel
                {
                    Title = "IT Weekend Ivano-Frankivsk",
                    Link = "https://itweekend.ua/en/announcements/itw-if-nov/",
                    Description = "Effective Memory Management: Memory Hygiene",
                    Collateral = new Dictionary<string, string>(3)
                    {
                        ["https://doc.co/LEvVSM"] = CommonStrings.CollateralPowerpoint
                    },
                    Location = "Ivano-Frankivsk (Ukraine)",
                    Date = new DateTime(2015, 11, 7)
                },
                new EventModel
                {
                    Title = "IT Weekend Rivne II",
                    Link = "https://itweekend.ua/en/announcements/itw-rv-oct/",
                    Description = "Web Development in Future",
                    Collateral = new Dictionary<string, string>(3)
                    {
                        ["https://sway.com/LxEYWidCXvki-OHN"] = CommonStrings.CollateralSway
                    },
                    Location = "Rivne (Ukraine)",
                    Date = new DateTime(2015, 10, 24)
                },
                new EventModel
                {
                    Title = CommonStrings.DotNetCommunitySoftserve,
                    Description = "Findings from .NET Microsoft Open Source Projects",
                    Collateral = new Dictionary<string, string>(3)
                    {
                        ["https://www.youtube.com/watch?v=m9MFhqq0g3k"] = CommonStrings.CollateralVideoRus,
                        ["https://doc.co/PU9YJa"] = CommonStrings.CollateralPowerpoint
                    },
                    Location = CommonStrings.LocationWebcast,
                    Date = new DateTime(2015, 10, 21)
                },
                new EventModel
                {
                    Title = "Pacemaker: .NET - SoftServe",
                    Description = "Effective Memory Management: Memory Hygiene",
                    Collateral = new Dictionary<string, string>(3)
                    {
                        ["https://www.youtube.com/watch?v=jpOgZBGL66g"] = CommonStrings.CollateralVideoRus,
                        ["https://doc.co/LEvVSM"] = CommonStrings.CollateralPowerpoint
                    },
                    Location = "Chernivtsi (Ukraine)",
                    Date = new DateTime(2015, 10, 17)
                },
                new EventModel
                {
                    Title = "Pacemaker: WebUI - SoftServe",
                    Description = "Web Development in Future",
                    Collateral = new Dictionary<string, string>(3)
                    {
                        ["https://doc.co/k8w18R"] = CommonStrings.CollateralSway
                    },
                    Location = "Kyiv (Ukraine)",
                    Date = new DateTime(2015, 9, 5)
                },
                new EventModel
                {
                    Title = "Web UI Community - SoftServe",
                    Description = "Typescript",
                    Collateral = new Dictionary<string, string>(3)
                    {
                        ["https://www.youtube.com/watch?v=UqustygnUgk"] = CommonStrings.CollateralVideoRus,
                        ["https://doc.co/k8w18R"] = CommonStrings.CollateralPowerpoint
                    },
                    Location = CommonStrings.LocationWebcast,
                    Date = new DateTime(2015, 7, 24)
                },
                new EventModel
                {
                    Title = "Development Process Community - SoftServe",
                    Description = "Git workflows for enterprise teams",
                    Collateral = new Dictionary<string, string>(3)
                    {
                        ["https://www.youtube.com/watch?v=RlJOX5bbh1U"] = CommonStrings.CollateralVideoRus,
                        ["https://doc.co/6XH7Kv"] = CommonStrings.CollateralPowerpoint
                    },
                    Location = CommonStrings.LocationWebcast,
                    Date = new DateTime(2015, 6, 2)
                },
                new EventModel
                {
                    Title = CommonStrings.DotNetCommunitySoftserve,
                    Description = "Making .Net Applications Faster",
                    Collateral = new Dictionary<string, string>(3)
                    {
                        ["https://www.youtube.com/watch?v=Rgvr1hynOmE"] = CommonStrings.CollateralVideoRus,
                        ["https://doc.co/8VaeeJ"] = CommonStrings.CollateralPowerpoint,
                        ["https://github.com/Ky7m/MakingDotNETApplicationsFaster"] = CommonStrings.CollateralDemoCode
                    },
                    Location = CommonStrings.LocationWebcast,
                    Date = new DateTime(2015, 2, 11)
                }
            };
        }
    }
}
