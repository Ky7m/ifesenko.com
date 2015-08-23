﻿using System.Configuration;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using PersonalHomePage.Services.CloudStorageService.Model;

namespace PersonalHomePage.Services.CloudStorageService
{
    public sealed class CloudStorageService
    {
        private readonly CloudTableClient _cloudTableClient;
        public CloudStorageService()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            var cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            _cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
        }


        public SettingTableEntity[] RetrieveAllSettingsForService(string serviceName)
        {
            var table = _cloudTableClient.GetTableReference("Settings");

            var query =
                new TableQuery<SettingTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey",
                    QueryComparisons.Equal, serviceName));

            return table.ExecuteQuery(query).ToArray();
        }

        public void ReplaceSettingValueForService(SettingTableEntity updateSettingTableEntity)
        {
            var table = _cloudTableClient.GetTableReference("Settings");

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
