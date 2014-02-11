using Contracts;
using Contracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor
{
    public class PaymentRecievedEvent : PaymentReceived
    {
        public PaymentRecievedEvent(Guid correlationId, Order order)
        {
            EventId = Guid.NewGuid();
            Timestamp = DateTimeOffset.Now;
            CorrelationId = correlationId;
            Order = order;
        }

        public Guid CorrelationId { get; set; }
        public Guid EventId { get; private set; }
        public DateTimeOffset Timestamp { get; private set; }
        public Order Order { get; private set; }
    }
}
