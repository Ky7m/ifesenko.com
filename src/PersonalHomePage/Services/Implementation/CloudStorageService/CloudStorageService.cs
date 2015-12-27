using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using PersonalHomePage.Services.Implementation.CloudStorageService.Model;
using PersonalHomePage.Services.Interfaces;

namespace PersonalHomePage.Services.Implementation.CloudStorageService
{
    public sealed class CloudStorageService : IStorageService
    {
        private readonly Lazy<CloudStorageAccount> _cloudTableClient = new Lazy<CloudStorageAccount>(() =>
        {
            var connectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            return CloudStorageAccount.Parse(connectionString);
        });

        public async Task<SettingTableEntity[]> RetrieveAllSettingsForServiceAsync(string serviceName)
        {
            var tableQuery = new TableQuery<SettingTableEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, serviceName));

            return await ExecuteQueryAsync("Settings", tableQuery);
        }

        public async Task ReplaceSettingValueForServiceAsync(SettingTableEntity updateSettingTableEntity)
        {
            var client = _cloudTableClient.Value.CreateCloudTableClient();
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
            var client = _cloudTableClient.Value.CreateCloudTableClient();
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

        public async Task<EventTableEntity[]> RetrieveAllEventsAsync()
        {
            return await ExecuteQueryAsync<EventTableEntity>("Events");
        }

        private async Task<T[]> ExecuteQueryAsync<T>(string tableName, TableQuery<T> tableQuery = null) where T : ITableEntity, new()
        {
            var result = new List<T>();
            var client = _cloudTableClient.Value.CreateCloudTableClient();
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
    }
}
