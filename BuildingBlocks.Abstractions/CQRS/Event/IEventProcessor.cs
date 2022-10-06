namespace BuildingBlocks.Abstractions.CQRS.Event;

/// <summary>
/// Internal Event Bus.
/// </summary>
public interface IEventProcessor
{

    /// <summary>
    /// Send the event to outbox for saving and publishing to broker in the background.
    /// </summary>
    /// <param name="event"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TEvent"></typeparam>
    /// <returns></returns>
    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IEvent;
    
    /// <summary>
    ///  Send the events to outbox for saving and publishing to broker in the background.
    /// </summary>
    /// <param name="events"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TEvent"></typeparam>
    /// <returns></returns>
    public Task PublishAsync<TEvent>(TEvent[] @events, CancellationToken cancellationToken = default)
        where TEvent : IEvent;


    /// <summary>
    /// Dispatch event internally to corresponding handler for executing.
    /// </summary>
    /// <param name="event"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TEvent"></typeparam>
    /// <returns></returns>
    public Task DispatchAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IEvent;
    
    /// <summary>
    /// Dispatch events internally to corresponding handler for executing.
    /// </summary>
    /// <param name="events"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TEvent"></typeparam>
    /// <returns></returns>
    public Task DispatchAsync<TEvent>(TEvent[] @events, CancellationToken cancellationToken = default)
        where TEvent : IEvent;
    
}