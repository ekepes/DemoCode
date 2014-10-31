using MassTransit;
using System;

namespace Contracts.Events
{
    public interface OrderPlaced : CorrelatedBy<Guid>
    {
        Guid EventId { get; }
        DateTimeOffset Timestamp { get; }
        Order Order { get; }
    }
}
