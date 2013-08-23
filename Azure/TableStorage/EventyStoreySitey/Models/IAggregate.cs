using System.Collections.Generic;

namespace EventyStoreySitey.Models
{
    public interface IAggregate
    {
        string AggregateId { get; }

        void Rehydrate(string aggregateId, 
            IEnumerable<IDomainEvent> eventStream);
    }
}