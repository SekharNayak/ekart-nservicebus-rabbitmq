using eKart.Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKart.Orders
{
    public class OrderHandler : IHandleMessages<ProcessOrderCommand>
    {
        public Task Handle(ProcessOrderCommand message, IMessageHandlerContext context)
        {
            //Do Something 
            //another event can be published
            return Task.Run(() => {
                Console.WriteLine($"Order processed for customer  { message.EmailId }");
            });
        }
    }
}
