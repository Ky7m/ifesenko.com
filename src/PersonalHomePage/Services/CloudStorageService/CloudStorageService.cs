using System.Configuration;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using PersonalHomePage.Services.CloudStorageService.Model;

namespace PersonalHomePage.Services.CloudStorageService
{
    public sealed class CloudStorageService
    {
        private readonly CloudStorageAccount _cloudStorageAccount;

        public CloudStorageService()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            _cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
        }


        public SettingTableEntity[] RetrieveAllSettingsForService(string serviceName)
        {
            var tableClient = _cloudStorageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference("Settings");

            var query =
                new TableQuery<SettingTableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey",
                    QueryComparisons.Equal, serviceName));

            return table.ExecuteQuery(query).ToArray();
        }
    }
}
