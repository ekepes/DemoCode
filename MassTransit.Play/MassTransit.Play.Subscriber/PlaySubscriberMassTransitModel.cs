namespace MassTransit.Play.Subscriber
{
    using System.Reflection;

    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;

    using Magnum.Data;
    using Magnum.ForNHibernate.Data;

    using MassTransit.NinjectIntegration;
    using MassTransit.Play.Subscriber.Consumers;
    using MassTransit.Play.Subscriber.Domain;
    using MassTransit.Play.Subscriber.Orm;
    using MassTransit.Play.Subscriber.Properties;
    using MassTransit.Transports.Msmq;

    using NHibernate;

    public class PlaySubscriberMassTransitModel : MassTransitModuleBase
    {
        private static ISessionFactory _SessionFactory;

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

            Assembly assembly = Assembly.LoadFrom(Settings.Default.MappingAssembly.Trim());
            _SessionFactory =
                Fluently.Configure().Database(
                    MsSqlConfiguration.MsSql2008.ConnectionString(Settings.Default.ConnectionString).AdoNetBatchSize(
                        100).DefaultSchema("dbo").Raw("prepare_sql", "true")).Mappings(
                            m => m.FluentMappings.AddFromAssembly(assembly)
                    // Uncomment next line to export mappings if you need them:
                    //.ExportTo(@"E:\POC\MAI.Prototype.PillBuddy\output")
                    ).BuildSessionFactory();

            NHibernateUnitOfWork.SetSessionProvider(() => _SessionFactory.OpenSession());

            UnitOfWork.SetUnitOfWorkProvider(NHibernateUnitOfWork.Create);

            Bind<NHibernateUnitOfWork>().ToMethod(cxt => (NHibernateUnitOfWork)NHibernateUnitOfWork.Create());

            Bind<NewCustomerMessageConsumer>().To<NewCustomerMessageConsumer>().InTransientScope();
            Bind<Orm.IRepository<AuditEvent>>().To<OrmRepository<AuditEvent>>().InTransientScope();
        }
    }
}