using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Commands
{
    public class ShipOrder : ICommand
    {
        public string OrderId { get; set; }
    }
}
