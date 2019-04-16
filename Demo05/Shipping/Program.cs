using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Demo05.Shipping
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Shipping";

            // Endpoint is a component with send/receive capabilities
            // Each endpoint need a name that identify it
            var endpointConfiguration = new EndpointConfiguration("Dem05.Shipping");

            // Learing transport is for begginers
            // NSserviceBus Create fake, file-based "queues" in a.learningtransport directory inside solution directory. 
            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            // Saga need to be persisted, so we need to configure it
            var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();

            // Endpoint need to start process
            // Use Configure wait in order to avoid capturing ans restoring Context 
            // More info:(https://msdn.microsoft.com/en-us/magazine/jj991977.aspx)
            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
