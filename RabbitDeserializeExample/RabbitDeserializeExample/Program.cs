using System;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitDeserializeExample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                //channel.QueueDeclare("patient_order_service_error", true, false, false, null);

                QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);
                channel.BasicConsume("patient_order_service_error", true, consumer);

                BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                byte[] body = ea.Body;
                string bodyString = Encoding.UTF8.GetString(body);

                var messageType = GetMessageType(bodyString);

                var type = Type.GetType(string.Format("{0}, SampleSOA.Messages", messageType));
                Console.WriteLine(type);

                var message = GetMessage(bodyString);
                Console.WriteLine(message);

                var deserializeObject = JsonConvert.DeserializeObject(message, type);
                Console.WriteLine(deserializeObject);

                Console.WriteLine("Done");
                Console.ReadLine();
            }
        }

        private static string GetMessageType(string bodyString)
        {
            JToken token = JObject.Parse(bodyString);
            var messageType = token.SelectToken("messageType").First.Value<string>();
            messageType = messageType.Replace("urn:message:", string.Empty);
            messageType = messageType.Replace(":", ".");
            return messageType;
        }

        private static string GetMessage(string bodyString)
        {
            JToken token = JObject.Parse(bodyString);

            var message = token.SelectToken("message");
            return message.ToString();
        }
    }
}