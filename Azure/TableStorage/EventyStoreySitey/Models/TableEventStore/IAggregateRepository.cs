namespace EventyStoreySitey.Models.TableEventStore
{
    public interface IAggregateRepository
    {
        void StoreEvent<TAggregate>(string tenantId,
            IDomainEvent myEvent)
            where TAggregate : IAggregate;

        TAggregate GetAggregate<TAggregate>(string tenantId,
            string aggregateId)
            where TAggregate : IAggregate, new();
    }
}