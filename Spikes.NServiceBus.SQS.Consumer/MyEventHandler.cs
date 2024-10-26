using NServiceBus.Logging;
using Spikes.NServiceBus.SQS.Contracts;

namespace Spikes.NServiceBus.SQS.Consumer;

public class MyEventHandler : IHandleMessages<MyEvent>
{
    static readonly ILog log = LogManager.GetLogger<MyEventHandler>();

    public Task Handle(MyEvent eventMessage, IMessageHandlerContext context)
    {
        log.Info($"Received {nameof(MyEvent)} with a payload of {eventMessage.Data?.Length ?? 0} bytes.");
        return Task.CompletedTask;
    }
}