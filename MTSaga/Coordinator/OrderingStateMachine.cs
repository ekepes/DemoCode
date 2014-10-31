using Automatonymous;
using Contracts.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskell;

namespace Coordinator
{
    public class OrderingStateMachine :
        AutomatonymousStateMachine<OrderingState>
    {
        public OrderingStateMachine()
        {
            State(() => Placed);
            State(() => Paid);
            State(() => Gathered);
            State(() => Packed);
            State(() => Shipped);

            Event(() => OrderSubmitted);
            Event(() => OrderPlaced);
            Event(() => ReceivedPayment);

            Initially(
                When(OrderSubmitted)
                    .Then((state, message) =>
                        {
                            Console.WriteLine("Order Placed: {0} ({1})", message.Order.OrderDetails, message.CorrelationId);

                            state.Created = DateTimeOffset.Now;
                            state.Order = message.Order;
                        })
                    .Publish((_, message) => new OrderPlacedEvent(message.CorrelationId, message.Order))
                    .TransitionTo(Placed));
            During(Placed,
                When(ReceivedPayment)
                    .Then((state, message) =>
                        {
                            Console.WriteLine("Payment Received: {0} ({1})", message.Order.OrderDetails, message.CorrelationId);

                            state.Created = DateTimeOffset.Now;
                        })
                    //.Publish((_, message) => blah)
                    .TransitionTo(Paid));
        }

        public State Placed { get; private set; }
        public State Paid { get; private set; }
        public State Gathered { get; private set; }
        public State Packed { get; private set; }
        public State Shipped { get; private set; }

        public Event<OrderSubmitted> OrderSubmitted { get; private set; }

        public Event<OrderPlaced> OrderPlaced { get; private set; }

        public Event<PaymentReceived> ReceivedPayment { get; private set; }
    }
}
