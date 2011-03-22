using System;
using MassTransit.Internal;
using MassTransit.Play.Messages;

namespace MassTransit.Play.Subscriber.Consumers
{
    using System.Threading;

    using MassTransit.Play.Subscriber.Domain;
    using MassTransit.Play.Subscriber.Orm;

    public class NewCustomerMessageConsumer : Consumes<NewCustomerMessage>.All, IBusService
    {
        private readonly IObjectBuilder _ObjectBuilder;
        private IServiceBus _Bus;
        private UnsubscribeAction _UnsubscribeAction;

        public NewCustomerMessageConsumer(IObjectBuilder objectBuilder)
        {
            _ObjectBuilder = objectBuilder;
        }

        public void Consume(NewCustomerMessage message)
        {
            string description = string.Format("Received a NewCustomerMessage with Name : '{0}'", message.Name);
            Console.WriteLine(description);

            using (IRepository<AuditEvent> repositoryInstance = _ObjectBuilder.GetInstance<IRepository<AuditEvent>>())
            {
                using (IRepository<AuditEvent> childRepository = _ObjectBuilder.GetInstance<IRepository<AuditEvent>>())
                {
                    Console.WriteLine("Thread.Name = {0}", Thread.CurrentThread.ManagedThreadId);
                }

                AuditEvent auditEvent = new AuditEvent();
                auditEvent.Description = description;
                auditEvent.EventDate = DateTime.Now;
                repositoryInstance.Save(auditEvent);
            }
        }

        public void Dispose()
        {
            _Bus.Dispose();
        }

        public void Start(IServiceBus bus)
        {
            _Bus = bus;
            _UnsubscribeAction = bus.Subscribe(this);
        }

        public void Stop()
        {
            _UnsubscribeAction();
        }
    }
}