using Automatonymous;
using Contracts.Commands;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskell;

namespace Coordinator
{
    public class OrderingStateMachine :
        AutomatonymousStateMachine<OrderingState>
    {
        public OrderingStateMachine()
        {
            State(() => Placed);
            State(() => Paid);
            State(() => Gathered);
            State(() => Packed);
            State(() => Shipped);

            Event(() => OrderPlaced);
            //Event(() => Retrieved);
            //Event(() => RetrieveFailed);
            //Event(() => NotFound);

            Initially(
                When(OrderPlaced)
                    .Then((state, message) =>
                        {
                            Console.WriteLine("Requested: {0} ({1})", message.NewOrder, message.CommandId);

                            state.Created = DateTimeOffset.Now;
                            state.Order = message.NewOrder;
                        })
                    .Then(() => new SendRetrieveImageCommandActivity())
                    .Publish((_, message) => new OrderPlacedEvent(message.CommandId, message.NewOrder))
                    .TransitionTo(Placed));

            //During(Placed,
            //    // this is to handle the contract of publishing the event but an existing request is 
            //    // already pending
            //    When(OrderPlaced)
            //        .Then(
            //            (state, message) =>
            //            { Console.WriteLine("Pending: {0} ({1})", state.SourceAddress, message.RequestId); })
            //        .Publish((_, message) => new ImageRequestedEvent(message.RequestId, message.SourceAddress)),
            //    // this event is observed when the service completes the image retrieval
            //    When(Retrieved)
            //        .Then((state, message) =>
            //            {
            //                Console.WriteLine("Retrieved: {0} ({1})", message.LocalAddress, state.CorrelationId);

            //                state.LastRetrieved = message.Timestamp;
            //                state.LocalAddress = message.LocalAddress;
            //                state.ContentType = message.ContentType;
            //                state.ContentLength = message.ContentLength;
            //            })
            //        .TransitionTo(Paid),
            //    When(RetrieveFailed)
            //        .Then((state, message) => state.Reason = message.Reason)
            //        .TransitionTo(Gathered)
            //        .Publish((_, message) => new ImageRequestFaultedEvent(message.SourceAddress, message.Reason))
            //    );

            //During(Paid,
            //    When(OrderPlaced)
            //        .Then((state, message) =>
            //            {
            //                Console.WriteLine("Available: {0} {2} ({1})", state.LocalAddress, message.RequestId,
            //                    state.SourceAddress);
            //            })
            //        .Publish((_, message) => new ImageRequestedEvent(message.RequestId, message.SourceAddress))
            //        .Publish((state, message) =>
            //                 new ImageRequestCompletedEvent(state.ContentLength.Value, state.ContentType,
            //                     state.LocalAddress, state.SourceAddress, state.LastRetrieved.Value)));
        }

        public State Placed { get; private set; }
        public State Paid { get; private set; }
        public State Gathered { get; private set; }
        public State Packed { get; private set; }
        public State Shipped { get; private set; }

        public Event<PlaceOrder> OrderPlaced { get; private set; }
        //public Event<ImageRetrieved> Retrieved { get; private set; }
        //public Event<ImageRetrievalFailed> RetrieveFailed { get; private set; }
        //public Event<ImageNotFound> NotFound { get; private set; }
    }

    public class SendRetrieveImageCommandActivity :
        Activity<OrderingState>
    {
        public void Accept(StateMachineInspector inspector)
        {
            inspector.Inspect(this, x => { });
        }

        public void Execute(Composer composer, OrderingState instance)
        {
            SendCommand(composer, instance);
        }

        public void Execute<T>(Composer composer, OrderingState instance, T value)
        {
            SendCommand(composer, instance);
        }

        void SendCommand(Composer composer, OrderingState instance)
        {
            composer.Execute(() =>
                {
                    Uri faultAddress = instance.Bus.Endpoint.Address.Uri;

                    IEndpoint endpoint = instance.Bus.GetEndpoint(_imageRetrievalServiceAddress);

                    endpoint.Send(new RetrieveImageCommand(instance.SourceAddress),
                        x => x.SetFaultAddress(faultAddress));
                });
        }
    }
}
