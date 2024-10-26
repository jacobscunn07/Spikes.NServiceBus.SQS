Console.Title = "SimpleReceiver";
var endpointConfiguration = new EndpointConfiguration("Spikes.NServiceBus.SQS.Consumer");
endpointConfiguration.EnableInstallers();
endpointConfiguration.UseSerialization<SystemJsonSerializer>();

var transport = new SqsTransport
{
    S3 = new S3Settings("squirlycatzrcool-111111111", "my/key/prefix")
};
endpointConfiguration.UseTransport(transport);

var endpointInstance = await Endpoint.Start(endpointConfiguration);
Console.WriteLine("Press any key to exit");
Console.ReadKey();
await endpointInstance.Stop();