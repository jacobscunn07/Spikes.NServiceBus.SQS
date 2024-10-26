namespace Spikes.NServiceBus.SQS.Contracts;

public class MyEvent : IEvent
{
    public byte[] Data { get; set; }
}