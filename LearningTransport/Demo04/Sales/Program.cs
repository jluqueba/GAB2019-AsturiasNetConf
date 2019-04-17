using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Demo04.Sales
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Sales";

            // Endpoint is a component with send/receive capabilities
            // Each endpoint need a name that identify it
            var endpointConfiguration = new EndpointConfiguration("Sales");

            // Configure recoverability
            var recoverability = endpointConfiguration.Recoverability();

            // In DEV mode for better debugging
            //recoverability.Immediate(options => options.NumberOfRetries(0)); 
            //recoverability.Delayed(options => 
            //{
            //    options.NumberOfRetries(0);
            //    options.TimeIncrease(TimeSpan.FromMinutes(0));
            //});
            
            // Even with custom policy
            //recoverability.CustomPolicy(Policies.MyCustomRetriesPolicies.MyCustomRetryPolicy);

            // Learing transport is for begginers
            // NSserviceBus Create fake, file-based "queues" in a.learningtransport directory inside solution directory. 
            var transport = endpointConfiguration.UseTransport<LearningTransport>();

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
