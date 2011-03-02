using MassTransit.NinjectIntegration;
using MassTransit.Play.Subscriber.Consumers;
using MassTransit.Transports;
using MassTransit.Transports.Msmq;

namespace MassTransit.Play.Subscriber
{
    public class PlaySubscriberMassTransitModel : MassTransitModuleBase
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

            RegisterServiceBus(@"msmq://localhost/mt_play_subscriber",
                               x =>
                                   {
                                       x.SetConcurrentConsumerLimit(1);

                                       ConfigureSubscriptionClient(@"msmq://localhost/mt_subscriptions", x);
                                   });

            Bind<NewCustomerMessageConsumer>().To<NewCustomerMessageConsumer>().InTransientScope();
        }
    }
}