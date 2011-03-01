using System;
using MassTransit.NinjectIntegration;
using MassTransit.Play.Subscriber.Consumers;
using MassTransit.Transports;
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

            PlayMassTransitModel massTransitModuleBase = new PlayMassTransitModel();
            NinjectObjectBuilder container = new NinjectObjectBuilder(new StandardKernel(massTransitModuleBase));

            //var container = new DefaultMassTransitContainer("windsor.xml")
            //    .Register(
            //        Component.For<NewCustomerMessageConsumer>().LifeStyle.Transient
            //    );

            IServiceBus bus = container.GetInstance<IServiceBus>();
            NewCustomerMessageConsumer consumer = container.GetInstance<NewCustomerMessageConsumer>();
            consumer.Start(bus);

            Console.ReadLine();
            Console.WriteLine("Stopping Subscriber");
            consumer.Stop();
        }
    }

    public class PlayMassTransitModel : MassTransitModuleBase
    {
        public override void Load()
        {
            base.Load();

            RegisterEndpointFactory(x =>
                                        {
                                            x.RegisterTransport<LoopbackEndpoint>();
                                            x.RegisterTransport<MulticastUdpEndpoint>();
                                            x.RegisterTransport<MsmqEndpoint>();
                                        });

            RegisterServiceBus(@"msmq://localhost/mt_mike_subscriber",
                               x =>
                                   {
                                       x.SetConcurrentConsumerLimit(1);

                                       ConfigureSubscriptionClient(@"msmq://localhost/mt_subscriptions", x);
                                   });

            Bind<NewCustomerMessageConsumer>().To<NewCustomerMessageConsumer>().InTransientScope();
        }
    }
}