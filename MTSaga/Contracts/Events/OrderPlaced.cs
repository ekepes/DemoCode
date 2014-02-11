using System;

namespace Contracts.Events
{
    public interface OrderPlaced
    {
        Guid EventId { get; }
        DateTime Timestamp { get; }
        Guid OriginatingCommandId { get; }
        Order Order { get; }
    }
}
