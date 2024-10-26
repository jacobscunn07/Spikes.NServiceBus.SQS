using NServiceBus.Logging;
using Spikes.NServiceBus.SQS.Contracts;

namespace Spikes.NServiceBus.SQS.Consumer;

public class MyCommandHandler : IHandleMessages<MyCommand>
{
    static readonly ILog log = LogManager.GetLogger<MyCommandHandler>();

    public Task Handle(MyCommand commandMessage, IMessageHandlerContext context)
    {
        log.Info($"Received {nameof(MyCommand)} with a payload of {commandMessage.Data?.Length ?? 0} bytes.");
        return Task.CompletedTask;
    }
}