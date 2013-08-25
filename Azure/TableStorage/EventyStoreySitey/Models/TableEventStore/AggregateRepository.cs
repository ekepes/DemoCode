using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace EventyStoreySitey.Models.TableEventStore
{
    public class AggregateRepository : IAggregateRepository
    {
        private static readonly Assembly ReferenceAssembly = Assembly.GetExecutingAssembly();

        private readonly CloudStorageAccount _storageAccount =
            CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

        public void StoreEvent<TAggregate>(IDomainEvent myEvent)
            where TAggregate : IAggregate
        {
            var table = ResolveCloudTable<TAggregate>();

            var entity = new DomainEventTableEntity(myEvent.AggregateId)
                {
                    ValueType = myEvent.GetType().FullName,
                    Value =
                        JsonConvert.SerializeObject(
                            myEvent)
                };

            var insertOperation = TableOperation.Insert(entity);

            table.Execute(insertOperation);
        }

        public TAggregate GetAggregate<TAggregate>(string aggregateId)
            where TAggregate : IAggregate, new()
        {
            var table = ResolveCloudTable<TAggregate>();

            var query = new TableQuery<DomainEventTableEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey",
                                                          QueryComparisons.Equal,
                                                          aggregateId));

            var genericTableEntities = table.ExecuteQuery(query);
            IList<IDomainEvent> domainEvents = genericTableEntities
                .Select(entity =>
                        JsonConvert.DeserializeObject(entity.Value,
                                                      ReferenceAssembly.GetType(entity.ValueType)))
                .Select(deserializeObject => deserializeObject as IDomainEvent)
                .ToList();

            var aggregate = new TAggregate();
            aggregate.Rehydrate(aggregateId, domainEvents);

            return aggregate;
        }

        private CloudTable ResolveCloudTable<TAggregate>()
            where TAggregate : IAggregate
        {
            var tableClient = _storageAccount.CreateCloudTableClient();

            var table = tableClient.GetTableReference((typeof (TAggregate)).Name);
            table.CreateIfNotExists();
            return table;
        }
    }
}