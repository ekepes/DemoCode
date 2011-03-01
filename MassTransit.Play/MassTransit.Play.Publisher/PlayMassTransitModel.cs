using MassTransit.NinjectIntegration;
using MassTransit.Transports;
using MassTransit.Transports.Msmq;

namespace MassTransit.Play.Publisher
{
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

            RegisterServiceBus(@"msmq://localhost/mt_mike_publisher", x =>
            {
                x.SetConcurrentConsumerLimit(1);

                ConfigureSubscriptionClient(@"msmq://localhost/mt_subscriptions", x);
            });
        }
    }
}