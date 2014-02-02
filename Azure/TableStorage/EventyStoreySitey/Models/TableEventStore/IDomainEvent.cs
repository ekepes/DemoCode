namespace EventyStoreySitey.Models.TableEventStore
{
    public interface IDomainEvent
    {
        string AggregateId { get; }
    }
}