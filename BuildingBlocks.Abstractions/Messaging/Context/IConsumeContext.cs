using System.Diagnostics;

namespace BuildingBlocks.Abstractions.Messaging.Context;

public interface IConsumeContext<out TMessage> : IConsumeContext
    where TMessage : class, IMessage
{
}

public interface IConsumeContext
{
    object Message { get; }
    IDictionary<string, object?> Headers { get; }
    ActivityContext? ParentContext { get; set; }
    Guid MessageId { get; }
    string MessageType { get; }
    ContextItems Items { get; }
    int PayloadSize { get; }
    ulong Version { get; }
    DateTime Created { get; }
}