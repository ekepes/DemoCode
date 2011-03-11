namespace MassTransit.Play.Subscriber
{
    using System;

    using MassTransit.NinjectIntegration;
    using MassTransit.Play.Subscriber.Consumers;
    using MassTransit.Transports.Msmq;

    using Ninject;

    internal class Program
    {
        private static IObjectBuilder BootstrapContainer()
        {
            StandardKernel kernel = new StandardKernel();
            NinjectObjectBuilder container = new NinjectObjectBuilder(kernel);
            PlaySubscriberMassTransitModel module = new PlaySubscriberMassTransitModel(container);
            kernel.Load(module);

            return container;
        }

        private static void Main()
        {
            Console.WriteLine("Starting Subscriber, hit return to quit");

            MsmqEndpointConfigurator.Defaults(config => { config.CreateMissingQueues = true; });

            IObjectBuilder container = BootstrapContainer();

            IServiceBus bus = container.GetInstance<IServiceBus>();
            NewCustomerMessageConsumer consumer = container.GetInstance<NewCustomerMessageConsumer>();
            consumer.Start(bus);

            Console.ReadLine();
            Console.WriteLine("Stopping Subscriber");
            consumer.Stop();
        }
    }
}