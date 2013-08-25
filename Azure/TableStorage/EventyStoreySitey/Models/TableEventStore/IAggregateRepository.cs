namespace EventyStoreySitey.Models.TableEventStore
{
    public interface IAggregateRepository
    {
        void StoreEvent<TAggregate>(IDomainEvent myEvent)
            where TAggregate : IAggregate;

        TAggregate GetAggregate<TAggregate>(string aggregateId)
            where TAggregate : IAggregate, new();
    }
}