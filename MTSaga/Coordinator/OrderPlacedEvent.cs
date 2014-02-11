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
        public OrderPlacedEvent(Guid originatingCommandId, Order order)
        {
            OriginatingCommandId = originatingCommandId;
            Order = order;
        }

        public Guid EventId { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Guid OriginatingCommandId { get; private set; }
        public Order Order { get; private set; }
    }
}
