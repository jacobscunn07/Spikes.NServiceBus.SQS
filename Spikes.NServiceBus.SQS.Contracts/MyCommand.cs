namespace Spikes.NServiceBus.SQS.Contracts;

public class MyCommand : ICommand
{
    public byte[] Data { get; set; }
}