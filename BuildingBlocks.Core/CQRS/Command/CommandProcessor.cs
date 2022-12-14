using BuildingBlocks.Abstractions.CQRS.Command;
using BuildingBlocks.Abstractions.Scheduler;
using MediatR;

namespace BuildingBlocks.Core.CQRS.Command;

public class CommandProcessor : ICommandProcessor
{
    private readonly IMediator _mediator;
    private readonly ICommandScheduler _commandScheduler;

    public CommandProcessor(IMediator mediator, ICommandScheduler commandScheduler)
    {
        _mediator = mediator;
        _commandScheduler = commandScheduler;
    }

    public Task<TResult> SendAsync<TResult>(ICommand<TResult> command,
        CancellationToken cancellationToken = default) where TResult : notnull
    {
        return _mediator.Send(command, cancellationToken);
    }

    public async Task ScheduleAsync(IInternalCommand internalCommand,
        CancellationToken cancellationToken = default)
    {
        await _commandScheduler.ScheduleAsync(internalCommand, cancellationToken);
    }

    public async Task ScheduleAsync(IInternalCommand[] internalCommands,
        CancellationToken cancellationToken = default)
    {
        foreach (var internalCommand in internalCommands)
        {
            await ScheduleAsync(internalCommand, cancellationToken);
        }
    }
}