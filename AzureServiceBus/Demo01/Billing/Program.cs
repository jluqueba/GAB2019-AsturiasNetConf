﻿using Microsoft.Extensions.Configuration;
using NServiceBus;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Billing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Billing";

			// Load configuration
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddUserSecrets<Program>();

			IConfigurationRoot configuration = builder.Build();

			// Endpoint is a component with send/receive capabilities
			// Each endpoint need a name that identify it
			var endpointConfiguration = new EndpointConfiguration("Billing");
			endpointConfiguration.EnableInstallers();

			// Azure ServiceBus is advance transport, supports native Pub/Sub, no need persistence
			var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
			transport.ConnectionString(configuration.GetConnectionString("AzureServiceBus"));

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
