using BuildingBlocks.Abstractions.CQRS.Events.Internal;

namespace BuildingBlocks.Core.CQRS.Event.Internal;

public abstract record DomainNotificationEvent : Event, IDomainNotificationEvent;