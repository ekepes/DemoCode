using System.Text;

using RabbitMQ.Client;

namespace RabbitProducer
{
    internal class Sender
    {
        public void SendMessage(string message)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare("hello", false, false, false, null);

                    byte[] body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", "hello", null, body);
                }
            }
        }
    }
}