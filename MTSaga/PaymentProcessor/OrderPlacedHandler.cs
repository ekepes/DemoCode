using Contracts;
using Contracts.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor
{
    public class OrderPlacedHandler : Consumes<OrderPlaced>.All
    {
        private readonly IServiceBus _bus;

        public OrderPlacedHandler(IServiceBus bus)
        {
            _bus = bus;
        }

        public void Consume(OrderPlaced orderPlacedEvent)
        {
            Console.WriteLine("Processed payment for event {0} for order {1}.", orderPlacedEvent.CorrelationId, orderPlacedEvent.Order.OrderDetails);

            _bus.Publish(new PaymentRecievedEvent(orderPlacedEvent.CorrelationId, orderPlacedEvent.Order));
        }
    }
}
