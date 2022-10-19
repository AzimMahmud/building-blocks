using System.Collections.Immutable;
using Ardalis.GuardClauses;
using BuildingBlocks.Abstractions.CQRS.Events;
using BuildingBlocks.Abstractions.CQRS.Events.Internal;
using BuildingBlocks.Abstractions.Messaging.PersistMessage;

namespace BuildingBlocks.Core.CQRS.Event;

public class DomainEventPublisher : IDomainEventPublisher
{
    private readonly IEventProcessor _eventProcessor;
    private readonly IMessagePersistenceService _messagePersistenceService;
    private readonly IDomainNotificationEventPublisher _domainNotificationEventPublisher;
    private readonly IDomainEventsAccessor _domainEventAccessor;
    private readonly IServiceProvider _serviceProvider;

    public DomainEventPublisher(
        IEventProcessor eventProcessor,
        IMessagePersistenceService messagePersistenceService,
        IDomainNotificationEventPublisher domainNotificationEventPublisher,
        IDomainEventsAccessor domainEventAccessor,
        IServiceProvider serviceProvider)
    {
        _eventProcessor = eventProcessor;
        _messagePersistenceService = messagePersistenceService;
        _domainNotificationEventPublisher = Guard.Against.Null(domainNotificationEventPublisher, nameof(domainNotificationEventPublisher));
        _domainEventAccessor = Guard.Against.Null(domainEventAccessor, nameof(domainEventAccessor));
        _serviceProvider = Guard.Against.Null(serviceProvider, nameof(serviceProvider));
    }
    
    public Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        return PublishAsync(new[] { domainEvent }, cancellationToken);
    }

    public async Task PublishAsync(IDomainEvent[] domainEvents, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(domainEvents, nameof(domainEvents));

        if (!domainEvents.Any())
        {
            return;
        }
        
        // https://github.com/dotnet-architecture/eShopOnContainers/issues/700#issuecomment-461807560
        // https://github.com/dotnet-architecture/eShopOnContainers/blob/e05a87658128106fef4e628ccb830bc89325d9da/src/Services/Ordering/Ordering.Infrastructure/OrderingContext.cs#L65
        // http://www.kamilgrzybek.com/design/how-to-publish-and-handle-domain-events/
        // http://www.kamilgrzybek.com/design/handling-domain-events-missing-part/
        // https://www.ledjonbehluli.com/posts/domain_to_integration_event/
        
        //Dispatch our domain events before commit
        IReadOnlyCollection<IDomainEvent> eventsToDispatch = domainEvents.ToList();

        if (!eventsToDispatch.Any())
        {
            eventsToDispatch = _domainEventAccessor.UnCommittedDomainEvents.ToImmutableList();
        }

        await _eventProcessor.DispatchAsync(eventsToDispatch.ToArray(), cancellationToken);

        var wrappedNotificationEvents = eventsToDispatch.GetWrappedDomainNotificationEvents().ToArray();

    }
}