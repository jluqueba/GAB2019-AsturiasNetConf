using Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo05.Shipping.Handlers
{
    public class ShipOrderHandler : IHandleMessages<ShipOrder>
    {
        static ILog log = LogManager.GetLogger<ShipOrderHandler>();

        public Task Handle(ShipOrder message, IMessageHandlerContext context)
        {
            log.Info($"Order [{message.OrderId}] - Successfully shipped.");
            return Task.CompletedTask;
        }
    }
}
