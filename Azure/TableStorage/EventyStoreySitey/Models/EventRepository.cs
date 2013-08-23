using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace EventyStoreySitey.Models
{
    public class EventRepository
    {
        private static readonly Assembly ReferenceAssembly = Assembly.GetExecutingAssembly();

        private readonly CloudStorageAccount _storageAccount =
            CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

        public void StoreEvent<TAggregate>(IDomainEvent myEvent)
            where TAggregate : IAggregate
        {
            CloudTable table = ResolveCloudTable<TAggregate>();

            var entity = new GenericTableEntity(myEvent.AggregateId)
                {
                    ValueType = myEvent.GetType().FullName,
                    Value =
                        JsonConvert.SerializeObject(
                            myEvent)
                };

            TableOperation insertOperation = TableOperation.Insert(entity);

            table.Execute(insertOperation);
        }

        public IList<IDomainEvent> GetEvents<TAggregate>(string aggregateId)
            where TAggregate : IAggregate
        {
            CloudTable table = ResolveCloudTable<TAggregate>();

            TableQuery<GenericTableEntity> query =
                new TableQuery<GenericTableEntity>()
                    .Where(TableQuery.GenerateFilterCondition("PartitionKey",
                                                              QueryComparisons.Equal,
                                                              aggregateId));

            IList<IDomainEvent> domainEvents = new List<IDomainEvent>();
            IEnumerable<GenericTableEntity> genericTableEntities = table.ExecuteQuery(query);
            foreach (GenericTableEntity entity in genericTableEntities)
            {
                object deserializeObject =
                    JsonConvert.DeserializeObject(entity.Value,
                                                  ReferenceAssembly.GetType(entity.ValueType));
                domainEvents.Add(deserializeObject as IDomainEvent);
            }

            return domainEvents;
        }

        private CloudTable ResolveCloudTable<TAggregate>()
            where TAggregate : IAggregate
        {
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(GetTableName(typeof (TAggregate)));
            table.CreateIfNotExists();
            return table;
        }

        private string GetTableName(Type type)
        {
            return type.Name;
        }
    }
}