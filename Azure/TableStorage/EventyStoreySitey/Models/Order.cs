using System.Collections.Generic;

namespace EventyStoreySitey.Models
{
    public class Order : IAggregate
    {
        public string AggregateId { get; private set; }

        public string CustomerName { get; set; }

        public List<Item> Items { get; private set; }

        public void Rehydrate(string aggregateId, IEnumerable<IDomainEvent> eventStream)
        {
            AggregateId = aggregateId;
            Items = new List<Item>();
            foreach (IDomainEvent domainEvent in eventStream)
            {
                if (domainEvent is OrderStarted)
                {
                    Process(domainEvent as OrderStarted);
                }
                if (domainEvent is ItemAdded)
                {
                    Process(domainEvent as ItemAdded);
                }
            }
        }

        private void Process(OrderStarted orderStarted)
        {
            CustomerName = orderStarted.CustomerName;
        }

        private void Process(ItemAdded itemAdded)
        {
            var item = Items.Find(x => x.ItemName == itemAdded.ItemName);
            if (item != null)
            {
                item.Quantity += itemAdded.Quantity;
            }
            else
            {
                Items.Add(new Item { ItemName = itemAdded.ItemName, Quantity = itemAdded.Quantity });
            }
        }
    }
}