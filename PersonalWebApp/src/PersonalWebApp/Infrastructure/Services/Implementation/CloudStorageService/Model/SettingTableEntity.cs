﻿using Microsoft.WindowsAzure.Storage.Table;

namespace ifesenko.com.Infrastructure.Services.Implementation.CloudStorageService.Model
{
    public sealed class SettingTableEntity : TableEntity
    {
        public SettingTableEntity(string serviceName, string settingName)
        {
            PartitionKey = serviceName;
            RowKey = settingName;
        }

        public SettingTableEntity() { }

        public string Value { get; set; }
    }
}
