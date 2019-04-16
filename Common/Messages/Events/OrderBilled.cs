using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Events
{
    public class OrderBilled : IEvent
    {
        public string OrderId { get; set; }
    }
}
