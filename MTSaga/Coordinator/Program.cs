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
        private static readonly OrderingStateMachine _machine;
        private static ISagaRepository<OrderingState> _repository;
        private static IServiceBus _bus;

        static void Main(string[] args)
        {
            // use an in-memory saga repository to keep things simple, although SQLite would work
            // nicely here as well.
            _repository = new InMemorySagaRepository<OrderingState>();
            _bus = ServiceBusFactory.New(x =>
            {
                x.ReceiveFrom(_address);
                x.UseRabbitMq(r =>
                {
                    r.ConfigureHost(_address, h =>
                    {
                        // set username/password if required
                        h.SetRequestedHeartbeat(1);
                    });
                });

                x.Subscribe(s =>
                {
                    // subscribe the state machine to the bus, using the saga repository
                    s.StateMachineSaga(_machine, _repository, r =>
                    {
                        // correlate the command to existing commands and use the 
                        // requestId for the correlationId of the saga if it does not exist
                        r.Correlate(_machine.OrderPlaced,
                            (state, message) => state.Order == message.NewOrder)
                         .SelectCorrelationId(message => message.CommandId);

                        //// specify non-CorrelationId based correlation for the events
                        //r.Correlate(_machine.Retrieved,
                        //    (state, message) => state.SourceAddress == message.SourceAddress);

                        //r.Correlate(_machine.NotFound,
                        //    (state, message) => state.SourceAddress == message.SourceAddress);

                        //r.Correlate(_machine.RetrieveFailed,
                        //    (state, message) => state.SourceAddress == message.SourceAddress);
                    });
                });
            });
        }
    }
}
