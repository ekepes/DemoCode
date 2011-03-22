namespace MassTransit.Play.Subscriber
{
    using System.Reflection;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using MassTransit.NinjectIntegration;
    using MassTransit.Play.Subscriber.Consumers;
    using MassTransit.Play.Subscriber.Domain;
    using MassTransit.Play.Subscriber.Orm;
    using MassTransit.Play.Subscriber.Properties;
    using MassTransit.Transports.Msmq;

    using NHibernate;

    using Ninject;

    public class PlaySubscriberMassTransitModel : MassTransitModuleBase
    {
        public PlaySubscriberMassTransitModel(IObjectBuilder builder)
            : base(builder, typeof(MsmqEndpoint))
        {
        }

        public override void Load()
        {
            base.Load();

            RegisterServiceBus(
                @"msmq://localhost/mt_play_subscriber",
                x =>
                    {
                        x.SetConcurrentConsumerLimit(1);

                        ConfigureSubscriptionClient(@"msmq://localhost/mt_subscriptions", x);
                    });

            Bind<ISessionFactory>().ToMethod(context => CreateSessionFactory()).InSingletonScope();
            Bind<ISession>().ToMethod(context => CreateSession()).InThreadScope();

            Bind<NewCustomerMessageConsumer>().To<NewCustomerMessageConsumer>().InTransientScope();
            Bind<IRepository<AuditEvent>>().To<OrmRepository<AuditEvent>>().InTransientScope();
        }

        private ISession CreateSession()
        {
            return Kernel.Get<ISessionFactory>().OpenSession();
        }

        private ISessionFactory CreateSessionFactory()
        {
            Assembly assembly = Assembly.LoadFrom(Settings.Default.MappingAssembly.Trim());
            ISessionFactory sessionFactory =
                Fluently.Configure().Database(
                    MsSqlConfiguration.MsSql2008.ConnectionString(Settings.Default.ConnectionString).AdoNetBatchSize(
                        100).DefaultSchema("dbo").Raw("prepare_sql", "true")).Mappings(
                            m => m.FluentMappings.AddFromAssembly(assembly)
                    // Uncomment next line to export mappings if you need them:
                    //.ExportTo(@"E:\POC\MAI.Prototype.PillBuddy\output")
                    ).BuildSessionFactory();

            return sessionFactory;
        }
    }
}