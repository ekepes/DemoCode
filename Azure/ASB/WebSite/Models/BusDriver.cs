using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;

namespace WebSite.Models
{
    public class BusDriver
    {
        public void SendMessage(string message)
        {
            string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            string queueName = "asbtestqueue";

            NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.QueueExists(queueName))
            {
                namespaceManager.CreateQueue(queueName);
            }

            MessagingFactory factory =
                MessagingFactory.CreateFromConnectionString(connectionString);

            // Initialize the connection to Service Bus Queue
            var client = factory.CreateQueueClient(queueName, ReceiveMode.PeekLock);

            client.Send(new BrokeredMessage(message));
        }
    }
}