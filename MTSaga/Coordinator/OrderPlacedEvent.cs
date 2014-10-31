using Contracts;
using Contracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coordinator
{
    public class OrderPlacedEvent : OrderPlaced
    {
        public OrderPlacedEvent(Guid correlationId, Order order)
        {
            CorrelationId = correlationId;
            EventId = Guid.NewGuid();
            Timestamp = DateTimeOffset.Now;
            Order = order;
        }

        public Guid CorrelationId { get; set; }
        public Guid EventId { get; private set; }
        public DateTimeOffset Timestamp { get; private set; }
        public Order Order { get; private set; }
    }
}
