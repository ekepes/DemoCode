using System;
using Contracts;

namespace Customer
{
    public class CustomerOrder : Order
    {
        public CustomerOrder(string orderDetails)
        {
            OrderDetails = orderDetails;
        }

        public string OrderDetails { get; private set; }
    }
}
