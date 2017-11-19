using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKart.Orders
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }


        static async Task AsyncMain() {

            Console.Title = "Order processor";
            var endPointConfiguration = new EndpointConfiguration("eKart.Order");
            endPointConfiguration.UsePersistence<InMemoryPersistence>();
            endPointConfiguration.UseTransport<RabbitMQTransport>().ConnectionStringName("NServiceBus/Transport");
            endPointConfiguration.EnableInstallers();
            endPointConfiguration.SendFailedMessagesTo("error");
            endPointConfiguration.PurgeOnStartup(true);


            // use conventions instead of IEvent , ICommand and IMessage marker interfaces 
            // as it is more elegant and better way to do things .
            var conventions = endPointConfiguration.Conventions();
            conventions.DefiningCommandsAs(t => t.Namespace.StartsWith("eKart") && t.Name.EndsWith("Command"));
            conventions.DefiningEventsAs(t => t.Namespace.StartsWith("eKart") && t.Name.EndsWith("Event"));
            


            var endPointInstance = await Endpoint.Start(endPointConfiguration).ConfigureAwait(false);

            try
            {
                Console.WriteLine("Press any key to exit ");
                Console.ReadLine();
            }
            catch (Exception)
            {

                throw;
            }
            finally {

                await endPointInstance.Stop().ConfigureAwait(false);
            }

        }
    }
}
