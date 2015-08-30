using System;
using System.Configuration;
using System.Linq;
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

        public SettingTableEntity[] RetrieveAllSettingsForService(string serviceName)
        {
            var client = _cloudTableClient.Value.CreateCloudTableClient();
            var table = client.GetTableReference("Settings");

            var query =
                new TableQuery<SettingTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey",
                    QueryComparisons.Equal, serviceName));

            return table.ExecuteQuery(query).ToArray();
        }

        public void ReplaceSettingValueForService(SettingTableEntity updateSettingTableEntity)
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
                  var retrievedResult = table.Execute(retrieveOperation);

                  var updateEntity = (SettingTableEntity)retrievedResult.Result;
                  if (updateEntity == null)
                  {
                      return;
                  }
                 
                  updateEntity.Value = newValue;
                  var updateOperation = TableOperation.Replace(updateEntity);
                  table.Execute(updateOperation);
              /*});*/
        }

        public ShortToLongUrlMapTableEntity RetrieveLongUrlMapForShortUrl(string shortUrl)
        {
            var client = _cloudTableClient.Value.CreateCloudTableClient();
            var table = client.GetTableReference("ShortToLongUrlsMap");

            var query =
                new TableQuery<ShortToLongUrlMapTableEntity>().Where(TableQuery.GenerateFilterCondition("RowKey",
                    QueryComparisons.Equal, shortUrl));

            var shortToLongUrlMapTableEntity = table.ExecuteQuery(query).ToArray().FirstOrDefault();
            if (shortToLongUrlMapTableEntity != null)
            {
                shortToLongUrlMapTableEntity.Stats++;
                var replaceOperation = TableOperation.Replace(shortToLongUrlMapTableEntity);
                table.Execute(replaceOperation);
            }

            return shortToLongUrlMapTableEntity;
        }
    }
}
