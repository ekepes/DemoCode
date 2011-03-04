using System;
using MassTransit.NinjectIntegration;
using MassTransit.Play.Messages;
using MassTransit.Transports.Msmq;
using Ninject;

namespace MassTransit.Play.Publisher
{
    public class Program
    {
        private static void Main()
        {
            Console.WriteLine("Starting Publisher");

            MsmqEndpointConfigurator.Defaults(config => { config.CreateMissingQueues = true; });

            IObjectBuilder container = BootstrapContainer();
            IServiceBus bus = container.GetInstance<IServiceBus>();

            string name;
            while ((name = GetName()) != "q")
            {
                NewCustomerMessage message = new NewCustomerMessage {Name = name};
                bus.Publish(message);

                Console.WriteLine("Published NewCustomerMessage with name {0}", message.Name);
            }

            Console.WriteLine("Stopping Publisher");
            container.Release(bus);
        }

        private static string GetName()
        {
            Console.WriteLine("Enter a name to publish (q to quit)");
            return Console.ReadLine();
        }

        private static IObjectBuilder BootstrapContainer()
        {
            StandardKernel kernel = new StandardKernel();
            NinjectObjectBuilder container = new NinjectObjectBuilder(kernel);
            PlayPublisherMassTransitModel module = new PlayPublisherMassTransitModel(container);
            kernel.Load(module);

            return container;
        }
    }
}