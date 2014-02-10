using Automatonymous;
using Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coordinator
{
    public class OrderingState : SagaStateMachineInstance
    {
        public OrderingState(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public OrderingState()
        {
        }

        /// <summary>
        /// When the state instance was created
        /// </summary>
        public DateTimeOffset Created { get; set; }

        public Guid CorrelationId { get; private set; }
        public IServiceBus Bus { get; set; }

        public State CurrentState { get; set; }

        /// <summary>
        /// If faulted, the reason why it faulted
        /// </summary>
        public string Reason { get; set; }

        public Order Order { get; set; }
    }
}
