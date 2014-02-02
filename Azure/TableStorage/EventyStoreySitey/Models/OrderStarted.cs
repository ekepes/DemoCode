using EventyStoreySitey.Models.TableEventStore;

namespace EventyStoreySitey.Models
{
    public class OrderStarted : IDomainEvent
    {
        public string AggregateId { get; set; }

        public string CustomerName { get; set; }
    }
}