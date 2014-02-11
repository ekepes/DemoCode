using System;
using Contracts;
using Contracts.Events;

namespace Customer
{
    public class OrderSubmittedEvent : OrderSubmitted
    {
        public OrderSubmittedEvent(Order newOrder)
        {
            CorrelationId = Guid.NewGuid();
            EventId = Guid.NewGuid();
            Timestamp = DateTimeOffset.Now;

            Order = newOrder;
        }

        public Guid CorrelationId { get; set; }

        public Guid EventId { get; private set; }

        public DateTimeOffset Timestamp { get; private set; }

        public Order Order { get; private set; }
    }
}
