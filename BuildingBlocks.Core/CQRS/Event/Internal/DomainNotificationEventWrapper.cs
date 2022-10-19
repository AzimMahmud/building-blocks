using BuildingBlocks.Abstractions.CQRS.Events.Internal;

namespace BuildingBlocks.Core.CQRS.Event.Internal;

public record DomainNotificationEventWrapper<TDomainEventType>(TDomainEventType DomainEvent) : DomainNotificationEvent
    where TDomainEventType : IDomainEvent;