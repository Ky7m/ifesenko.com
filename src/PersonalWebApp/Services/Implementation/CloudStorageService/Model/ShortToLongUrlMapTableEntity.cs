using Microsoft.WindowsAzure.Storage.Table;

namespace PersonalWebApp.Services.Implementation.CloudStorageService.Model
{
    public sealed class ShortToLongUrlMapTableEntity : TableEntity
    {
        public ShortToLongUrlMapTableEntity(string categoryName, string shortUrl)
        {
            PartitionKey = categoryName;
            RowKey = shortUrl;
        }

        public ShortToLongUrlMapTableEntity() { }

        public string Target { get; set; }
        public string Description { get; set; }
        public int Stats { get; set; }
    }
}
