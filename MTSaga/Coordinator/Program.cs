using Automatonymous;
using MassTransit;
using MassTransit.Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coordinator
{
    class Program
    {
        private static readonly Uri _address = new Uri("rabbitmq://localhost/mtsaga-coordinator");
        private static readonly OrderingStateMachine _machine = new OrderingStateMachine();
        private static ISagaRepository<OrderingState> _repository;
        private static IServiceBus _bus;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Coordinator...");

            // use an in-memory saga repository to keep things simple, although SQLite would work
            // nicely here as well.
            _repository = new SagaRepositoryLearningShim<OrderingState>();
            _bus = ServiceBusFactory.New(x =>
            {
                x.ReceiveFrom(_address);
                x.UseRabbitMq(r =>
                {
                    r.ConfigureHost(_address, h =>
                    {
                        h.SetRequestedHeartbeat(1);
                    });
                });

                x.Subscribe(s =>
                {
                    s.StateMachineSaga(_machine, _repository);
                });
            });

            Console.WriteLine("Running - press Enter to stop...");
            Console.ReadLine();
        }
    }
}
