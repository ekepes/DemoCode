using System;

using RabbitMQ.Client;

namespace RabbitConsumer
{
    class Program
    {
        public static void Main()
        {
            var factory = new ConnectionFactory {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("hello", false, false, false, null);

                var consumer = new QueueingBasicConsumer(channel);
                channel.BasicConsume("hello", true, consumer);

                Console.WriteLine(" [*] Waiting for messages. To exit press <Ctrl-C>");
                
                while (true)
                {
                    var ea = consumer.Queue.Dequeue();

                    var body = ea.Body;
                    var message = System.Text.Encoding.UTF8.GetString(body);
                    
                    Console.WriteLine(" [x] Received {0}", message);
                }
            }
        }
    }
}
