namespace BuildingBlocks.Abstractions.CQRS.Event.Internal;

public interface IDomainNotificationEvent<TDomainEvent> : IDomainNotificationEvent where TDomainEvent : IDomainEvent
{
    TDomainEvent DomainEvent { get; set; }
}

public interface IDomainNotificationEvent : IEvent
{
}