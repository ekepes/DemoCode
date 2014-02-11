using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

using MassTransit;

using MessageContracts;

namespace PatientOrderService
{
    public class ItemPickedHandler : Consumes<ItemPicked>.All
    {
        public void Consume(ItemPicked message)
        {
            StoreEvent("ItemPicks", message.EventTime.ToString("yyyyMMddhhmmssffffff"), message);
        }

        private readonly CloudStorageAccount _storageAccount =
            CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

        private void StoreEvent(string tableName,
            string aggregateId,
            object myEvent)
        {
            var table = ResolveCloudTable(tableName);

            var entity = new DomainEventTableEntity(aggregateId)
            {
                ValueType = myEvent.GetType().FullName,
                Value =
                    JsonConvert.SerializeObject(
                        myEvent)
            };

            var insertOperation = TableOperation.Insert(entity);

            table.Execute(insertOperation);
        }

        private CloudTable ResolveCloudTable(string tableName)
        {
            var tableClient = _storageAccount.CreateCloudTableClient();

            var table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
            return table;
        }
    }
}
