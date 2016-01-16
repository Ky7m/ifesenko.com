using System;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using PersonalHomePage.Services.Implementation.CloudStorageService.Model;
using Xunit;

namespace PersonalHomePage.Tests
{
    public sealed class EventsIntegrationTests
    {
        private readonly Lazy<CloudStorageAccount> _storageAccount = new Lazy<CloudStorageAccount>(() =>
        {
            var connectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            return CloudStorageAccount.Parse(connectionString);
        });

        [Fact]
        public void AddEvent()
        {
            var tableClient = _storageAccount.Value.CreateCloudTableClient();
            var table = tableClient.GetTableReference("Events");
            var entity = new EventTableEntity("NDC OSLO", "Test name")
            {
                DateStart = DateTime.UtcNow.Date,
                DateEnd = DateTime.UtcNow.Date,
                EventUri = "http://ndcoslo.com",
                Place = "Norway, Oslo",
                SampleCodeUri = string.Empty,
                VideoUri = string.Empty,
                PresentationUri = string.Empty
            };

            var insertOperation = TableOperation.InsertOrReplace(entity);
            table.Execute(insertOperation);
        }
    }
}
