using Microsoft.Extensions.Hosting;
using Spikes.NServiceBus.SQS.Contracts;

// var host = Host.CreateDefaultBuilder()
//     // .UseSerilog((context, configuration) =>
//     //     configuration.ReadFrom.Configuration(context.Configuration))
//     
//     // .ConfigureNServiceBus(typeof(AssemblyMarker).Assembly.GetName().Name!)
//     // .ConfigureServices((ctx, services) =>
//     // {
//     //     services
//     //         .AddDataServices(ctx.Configuration)
//     //         .AddApplicationServices()
//     //         .AddQuartz()
//     //         .AddSingleton<IMessageBus, NServiceBusMessageBus>()
//     //         .AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))
//     //         ;
//     // })
//     .Build();
//
// await host.RunAsync();



var profile = Environment.GetEnvironmentVariable("AWS_PROFILE");

// Endpoint Configuration

var endpointConfiguration = new EndpointConfiguration("Spikes.NServiceBus.SQS.Publisher");
endpointConfiguration.UseSerialization<SystemJsonSerializer>();

var transport = new SqsTransport
{
    S3 = new S3Settings("squirlycatzrcool-111111111", "my/key/prefix")
};

endpointConfiguration.EnableInstallers();

var routing = endpointConfiguration.UseTransport(transport);

//

routing.RouteToEndpoint(typeof(MyCommand), "Spikes.NServiceBus.SQS.Consumer");
endpointConfiguration.EnableInstallers();

var endpointInstance = await Endpoint.Start(endpointConfiguration);

await SendMessages(endpointInstance);

await endpointInstance.Stop();

//

static async Task SendMessages(IMessageSession messageSession)
{
    Console.WriteLine(@"Press
[1] to send a command
[2] to send a command with a large body
[3] to publish an event
[4] to publish an event with a large body
[Esc] to exit.");

    while (true)
    {
        var input = Console.ReadKey();
        Console.WriteLine();

        switch (input.Key)
        {
            case ConsoleKey.D1:
                await messageSession.Send(new MyCommand());
                break;
            case ConsoleKey.D2:
                await messageSession.Send(new MyCommand
                {
                    Data = new byte[257 * 1024]
                });
                break;
            case ConsoleKey.D3:
                await messageSession.Publish(new MyEvent());
                break;
            case ConsoleKey.D4:
                await messageSession.Publish(new MyEvent()
                {
                    Data = new byte[257 * 1024]
                });
                break;
            case ConsoleKey.Escape:
                return;
        }
    }
}
