using System;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

using Newtonsoft.Json;

namespace EventyStoreySitey.Models
{
    public class EventRepository
    {
        private readonly CloudStorageAccount _storageAccount =
            CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

        public void StoreEvent<T>(T myEvent) where T : IDomainEvent
        {
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(GetTableName(myEvent.AggregateType));
            table.CreateIfNotExists();

            GenericTableEntity entity = new GenericTableEntity(myEvent.AggregateId)
                                            {
                                                Value =
                                                    JsonConvert.SerializeObject(
                                                        myEvent)
                                            };

            TableOperation insertOperation = TableOperation.Insert(entity);

            table.Execute(insertOperation);
        }

        private string GetTableName(Type type)
        {
            return type.FullName.Replace(".", "");
        }
    }
}