using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Events
{
    public class OrderPlaced : IEvent
    {
        public string OrderId { get; set; }
    }
}
