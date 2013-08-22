using System;

using Microsoft.WindowsAzure.Storage.Table;

namespace EventyStoreySitey.Models
{
    public class GenericTableEntity : TableEntity
    {
        public GenericTableEntity()
        {
        }

        public GenericTableEntity(string aggregateId)
        {
            PartitionKey = aggregateId;
            RowKey = DateTimeOffset.UtcNow.ToString("yyyyMMddhhmmssffffff");
        }

        public string Value { get; set; }
    }
}