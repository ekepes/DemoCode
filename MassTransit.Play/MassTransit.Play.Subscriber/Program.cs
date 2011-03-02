using System;
using MassTransit.NinjectIntegration;
using MassTransit.Play.Subscriber.Consumers;
using MassTransit.Transports.Msmq;
using Ninject;

namespace MassTransit.Play.Subscriber
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Starting Subscriber, hit return to quit");

            MsmqEndpointConfigurator.Defaults(config => { config.CreateMissingQueues = true; });

            PlaySubscriberMassTransitModel subscriberMassTransitModuleBase = new PlaySubscriberMassTransitModel();
            NinjectObjectBuilder container = new NinjectObjectBuilder(new StandardKernel(subscriberMassTransitModuleBase));
            
            IServiceBus bus = container.GetInstance<IServiceBus>();
            NewCustomerMessageConsumer consumer = container.GetInstance<NewCustomerMessageConsumer>();
            consumer.Start(bus);

            Console.ReadLine();
            Console.WriteLine("Stopping Subscriber");
            consumer.Stop();
        }
    }
}