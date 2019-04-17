using Messages.Commands;
using Microsoft.Extensions.Configuration;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ClientUI
{
    class Program
    {
        static ILog log = LogManager.GetLogger<Program>();

        static async Task Main(string[] args)
        {
            Console.Title = "ClientUI";

			// Load configuration
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			IConfigurationRoot configuration = builder.Build();

			var defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Info);

            // Endpoint is a component with send/receive capabilities
            // Each endpoint need a name that identify it
            var endpointConfiguration = new EndpointConfiguration("ClientUI");
			endpointConfiguration.EnableInstallers();

			// Azure ServiceBus is advance transport, supports native Pub/Sub, no need persistence
			var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
			transport.ConnectionString(configuration.GetConnectionString("AzureServiceBus"));

			//Specifiy that PlaceOrder must be send to Sales endpoint
			var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");

            // Endpoint need to start process
            // Use Configure wait in order to avoid capturing ans restoring Context 
            // More info:(https://msdn.microsoft.com/en-us/magazine/jj991977.aspx)
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            // Replace with:
            await RunLoop(endpointInstance).ConfigureAwait(false);

            // End point end process
            await endpointInstance.Stop().ConfigureAwait(false);
        }

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                log.Info("Press 'P' to place an order, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        // Instantiate the command
                        var command = new PlaceOrder{ OrderId = Guid.NewGuid().ToString() };
                        // Send the command to the local endpoint
                        log.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");
                        // "Local" means not send to an external endpoint
                        //await endpointInstance.SendLocal(command).ConfigureAwait(false);
                        await endpointInstance.Send(command);
                        break;

                    case ConsoleKey.Q:
                        return;

                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }
    }
}
