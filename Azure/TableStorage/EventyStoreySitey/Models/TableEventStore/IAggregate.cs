using System.Collections.Generic;

namespace EventyStoreySitey.Models.TableEventStore
{
    public interface IAggregate
    {
        string TenantId { get; }

        string AggregateId { get; }

        void Rehydrate(string tenantId,
            string aggregateId, 
            IEnumerable<IDomainEvent> eventStream);
    }
}