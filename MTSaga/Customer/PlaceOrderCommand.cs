using System;
using Contracts;
using Contracts.Commands;

namespace Customer
{
    public class PlaceOrderCommand : PlaceOrder
    {
        public PlaceOrderCommand(Order newOrder)
        {
            CommandId = Guid.NewGuid();
            Timestamp = DateTimeOffset.Now;

            NewOrder = newOrder;
        }

        public Guid CommandId { get; private set; }

        public DateTimeOffset Timestamp { get; private set; }

        public Order NewOrder { get; private set; }
    }
}
