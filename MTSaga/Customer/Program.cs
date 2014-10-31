using Contracts;
using MassTransit;
using System;

namespace Customer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting up service bus...");
            RegisterServiceBus();
            Console.WriteLine("Bus started.");

            while (true)
            {
                Console.WriteLine("Enter order (or empty string to quit):");
                string order = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(order))
                {
                    break;
                }

                Bus.Instance.Publish(new OrderSubmittedEvent(new CustomerOrder(order)));
            }

            StopServiceBus();
        }

        //public static readonly Uri CoordinatorAddress = new Uri("rabbitmq://localhost/mtsaga-coordinator");
        public static readonly Uri WebAddress = new Uri("rabbitmq://localhost/mtsaga-customer");

        public static void RegisterServiceBus()
        {
            Bus.Initialize(x =>
            {
                x.UseRabbitMq(r =>
                {
                    r.ConfigureHost(WebAddress, h =>
                    {
                        // set username/password if required
                        h.SetRequestedHeartbeat(1);
                    });
                });
                x.ReceiveFrom(WebAddress);
            });
        }

        public static void StopServiceBus()
        {
            Bus.Shutdown();
        }
    }
}
