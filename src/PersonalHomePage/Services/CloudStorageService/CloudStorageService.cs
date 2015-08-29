using System;
using System.Configuration;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using PersonalHomePage.Services.CloudStorageService.Model;

namespace PersonalHomePage.Services.CloudStorageService
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

            var retrievedResult = table.Execute(retrieveOperation);

            var updateEntity = (SettingTableEntity)retrievedResult.Result;

            if (updateEntity != null)
            {
                updateEntity.Value = updateSettingTableEntity.Value;
                var updateOperation = TableOperation.Replace(updateEntity);
                table.Execute(updateOperation);
            }
        }
    }
}
