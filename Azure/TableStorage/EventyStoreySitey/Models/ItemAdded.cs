using EventyStoreySitey.Models.TableEventStore;

namespace EventyStoreySitey.Models
{
    public class ItemAdded : IDomainEvent
    {
        public string AggregateId { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }
    }
}