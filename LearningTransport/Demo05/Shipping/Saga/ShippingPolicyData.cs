using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shipping.Saga
{
    public class ShippingPolicyData : ContainSagaData
    {
        public string OrderId { get; set; }
        public bool IsOrderPlaced { get; set; }
        public bool IsOrderBilled { get; set; }
    }
}
