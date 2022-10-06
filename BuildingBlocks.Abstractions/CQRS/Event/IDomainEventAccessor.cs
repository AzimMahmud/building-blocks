using BuildingBlocks.Abstractions.CQRS.Event.Internal;

namespace BuildingBlocks.Abstractions.CQRS.Event;

public interface IDomainEventAccessor
{
    IReadOnlyCollection<IDomainEvent> UnCommittedDomainEvents { get; }
}