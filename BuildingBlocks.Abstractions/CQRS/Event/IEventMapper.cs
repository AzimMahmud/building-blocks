using BuildingBlocks.Abstractions.CQRS.Event.Internal;
using BuildingBlocks.Abstractions.Messaging;

namespace BuildingBlocks.Abstractions.CQRS.Event;

public interface IEventMapper : IDomainNotificationEventMapper, IIntegrationEventMapper
{
    
}


public interface IDomainNotificationEventMapper
{
    IReadOnlyList<IDomainNotificationEvent?> MapToDomainNotificationEvents(IReadOnlyList<IDomainEvent> domainEvents);
    IDomainNotificationEvent? MapToDomainNotificationEvent(IDomainEvent domainEvent);
}

public interface IIntegrationEventMapper
{
    IReadOnlyList<IIntegrationEvent?> MapToIntegrationEvents(IReadOnlyList<IDomainEvent> domainEvents);
    IIntegrationEvent? MapToIntegrationEvent(IIntegrationEvent domainEvent);
}