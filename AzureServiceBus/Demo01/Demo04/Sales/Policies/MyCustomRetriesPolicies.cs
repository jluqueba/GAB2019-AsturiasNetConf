using NServiceBus;
using NServiceBus.Transport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo04.Sales.Policies
{
    public class MyCustomRetriesPolicies
    {
        public static RecoverabilityAction MyCustomRetryPolicy(RecoverabilityConfig config, ErrorContext context)
        {
            var action = DefaultRecoverabilityPolicy.Invoke(config, context);

            // If not a retry then continue
            if (!(action is DelayedRetry delayedRetryAction))
                return action;

            // Check for concrete exception or business custom exception
            if (context.Exception is InvalidCastException)
                return RecoverabilityAction.MoveToError(config.Failed.ErrorQueue);
            
            // Override default delivery delay.
            return RecoverabilityAction.DelayedRetry(TimeSpan.FromSeconds(5));
        }
    }
}
