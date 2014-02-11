using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace PatientOrderService
{
    public class DomainEventTableEntity : TableEntity
    {
        public DomainEventTableEntity()
        {
        }

        public DomainEventTableEntity(string aggregateId)
        {
            PartitionKey = aggregateId;
            RowKey = DateTimeOffset.UtcNow.ToString("yyyyMMddhhmmssffffff");
        }

        public string ValueType { get; set; }

        public string Value { get; set; }
    }
}