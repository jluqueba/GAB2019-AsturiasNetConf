using Messages.Commands;
using Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Handlers
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        // It´s an expensive call so implent it as static
        static ILog log = LogManager.GetLogger<PlaceOrderHandler>();

        // Random for transient exception
        static Random random = new Random();

        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            log.Info($"Received PlaceOrder, OrderId = {message.OrderId}");

            // Throw exception in order to check retries
            // This is a systemic exception
            //throw new Exception("BOOM");

            // This is a transient exception %20 probabilities
            //if (random.Next(0,5) == 0)
                //throw new Exception("BOOM");

            var orderPlaced = new OrderPlaced() { OrderId = message.OrderId };

            //This is the way to publich an event inside a handler
            return context.Publish(orderPlaced);
        }
    }
}
