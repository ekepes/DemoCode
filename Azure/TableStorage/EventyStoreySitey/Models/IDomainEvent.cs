namespace EventyStoreySitey.Models
{
    public interface IDomainEvent
    {
        string AggregateId { get; }
    }
}