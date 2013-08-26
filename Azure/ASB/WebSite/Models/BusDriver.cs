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

            NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);

            if (!namespaceManager.QueueExists("TestQueue"))
            {
                namespaceManager.CreateQueue("TestQueue");
            }

            QueueClient client = QueueClient.CreateFromConnectionString(connectionString, "TestQueue");

            client.Send(new BrokeredMessage(message));
        }
    }
}