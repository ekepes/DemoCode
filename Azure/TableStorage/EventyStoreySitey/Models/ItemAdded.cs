using System;

namespace EventyStoreySitey.Models
{
    public class ItemAdded : IDomainEvent
    {
        public string AggregateId { get; set; }

        public Type AggregateType
        {
            get
            {
                return typeof(Order);
            }
        }

        public string ItemName { get; set; }

        public int Quantity { get; set; }
    }
}