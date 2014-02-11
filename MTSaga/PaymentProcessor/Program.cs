using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor
{
    class Program
    {
        static IServiceBus _bus;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting up service bus...");
            RegisterServiceBus();
            Console.WriteLine("Bus started.");

            Console.WriteLine("Listening for payments to process. Press Enter to quit...");

            Console.ReadLine();

            StopServiceBus();
        }

        //public static readonly Uri CoordinatorAddress = new Uri("rabbitmq://localhost/mtsaga-coordinator");
        public static readonly Uri WebAddress = new Uri("rabbitmq://localhost/mtsaga-paymentprocessor");

        public static void RegisterServiceBus()
        {
            _bus = ServiceBusFactory.New(sbc =>
            {
                sbc.UseRabbitMq(r =>
                {
                    r.ConfigureHost(WebAddress, h =>
                    {
                        // set username/password if required
                        h.SetRequestedHeartbeat(1);
                    });
                });
                sbc.ReceiveFrom(WebAddress); 
            });
            _bus.SubscribeInstance(new OrderPlacedHandler(_bus));
        }

        public static void StopServiceBus()
        {
            _bus.Dispose();
        }
    }
}
