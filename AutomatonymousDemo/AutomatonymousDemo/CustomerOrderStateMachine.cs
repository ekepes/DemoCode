using Automatonymous;
namespace AutomatonymousDemo
{
    public class CustomerOrderStateMachine :
            AutomatonymousStateMachine<CustomerOrder>
    {
        public CustomerOrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            State(() => Placed);
            State(() => ReadyToPack);
            State(() => Packed);
            State(() => Shipped);

            Event(() => OrderPlaced);
            Event(() => PaymentReceived);
            Event(() => OrderPaid);
            Event(() => OrderGathered);
            Event(() => OrderPrepared, x => x.PreparationStatus, OrderPaid, OrderGathered);

            Event(() => OrderPacked);
            Event(() => OrderShipped);
            Event(() => OrderClosed);

            During(Initial,
                When(OrderPlaced)
                    .TransitionTo(Placed));

            During(Placed,
                When(PaymentReceived)
                    .Then((instance, payment) => instance.Pay(payment)),
                When(OrderPrepared)
                    .TransitionTo(ReadyToPack));

            During(ReadyToPack,
                When(OrderPacked)
                    .TransitionTo(Packed));
            During(Packed,
                When(OrderShipped)
                    .TransitionTo(Shipped));
            During(Shipped,
                When(OrderClosed)
                    .Finalize());
        }

        public State Placed { get; private set; }
        public State ReadyToPack { get; private set; }
        public State Packed { get; private set; }
        public State Shipped { get; private set; }

        public Event OrderPlaced { get; private set; }
        public Event<decimal> PaymentReceived { get; private set; }
        public Event OrderPaid { get; private set; }
        public Event OrderGathered { get; private set; }
        public Event OrderPrepared { get; private set; }
        public Event OrderPacked { get; private set; }
        public Event OrderShipped { get; private set; }
        public Event OrderClosed { get; private set; }

        public CustomerOrder CreateOrder(string customerName,
            int itemsOrdered,
            decimal amountOwed)
        {
            return new CustomerOrder(this, customerName, itemsOrdered, amountOwed);
        }
    }
}
