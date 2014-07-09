using System.Text;

using RabbitMQ.Client;

namespace RabbitProducer
{
    public class Sender
    {
        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory {HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("hello", false, false, false, null);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", "hello", null, body);
                }
            }
        }
    }
}