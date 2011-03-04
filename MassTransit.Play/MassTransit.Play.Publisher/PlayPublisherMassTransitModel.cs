using MassTransit.NinjectIntegration;
using MassTransit.Transports.Msmq;

namespace MassTransit.Play.Publisher
{
    public class PlayPublisherMassTransitModel : MassTransitModuleBase
    {
        public PlayPublisherMassTransitModel(IObjectBuilder builder)
            : base(builder, typeof(MsmqEndpoint))
        {
        }

        public override void Load()
        {
            base.Load();

            RegisterServiceBus(@"msmq://localhost/mt_play_publisher",
                               x =>
                                   {
                                       x.SetConcurrentConsumerLimit(1);

                                       ConfigureSubscriptionClient(@"msmq://localhost/mt_subscriptions", x);
                                   });
        }
    }
}