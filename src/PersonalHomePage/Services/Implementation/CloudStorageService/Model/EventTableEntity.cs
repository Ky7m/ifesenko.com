using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace PersonalHomePage.Services.Implementation.CloudStorageService.Model
{
    public sealed class EventTableEntity : TableEntity
    {
        public EventTableEntity(string conferenceName, string presentationName)
        {
            PartitionKey = conferenceName;
            RowKey = presentationName;
        }

        public EventTableEntity() { }

        public string Place { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public string EventUri { get; set; }

        public string PresentationUri { get; set; }
        public string VideoUri { get; set; }
        public string SampleCodeUri { get; set; }
    }
}