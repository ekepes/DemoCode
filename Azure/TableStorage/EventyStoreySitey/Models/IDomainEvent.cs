using System;

namespace EventyStoreySitey.Models
{
    public interface IDomainEvent
    {
        string AggregateId { get; }

        Type AggregateType { get; }
    }
}