using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Domain;

namespace BuildingBlocks.Abstractions.CQRS.Event;

public interface IAggregatesDomainEventsRequestStore
{
    IReadOnlyList<IDomainEvent> AddEventsForAggregate<T>(T aggregate) where T : IHaveAggregate;
    void AddEvents(IReadOnlyList<IDomainEvent> events);
    IReadOnlyList<IDomainEvent> GetAllUncommittedEvents();
}