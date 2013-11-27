using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

using ScaleOutContracts;

namespace WorkCreator
{
    public class PickCreator
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudQueueClient _queueClient;
        private readonly CloudQueue _queue;

        public PickCreator()
        {
            _storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));
            
            _queueClient = _storageAccount.CreateCloudQueueClient();
        
            _queue = _queueClient.GetQueueReference("scaleoutsamplequeue");

            _queue.CreateIfNotExists();
        }

        public void CreatePick()
        {
            Pick pick = new Pick("Cocaine", 2);

            CloudQueueMessage message = new CloudQueueMessage(pick.SerializedPickString());
            _queue.AddMessage(message);
        }
    }
}