namespace BuildingBlocks.Abstractions.CQRS.Event.Internal;

public interface IDomainEventContext
{
    IReadOnlyCollection<IDomainEvent> GetAllUnCommittedEvents();
    void MarkUnCommittedDomainEventAsCommitted();
}