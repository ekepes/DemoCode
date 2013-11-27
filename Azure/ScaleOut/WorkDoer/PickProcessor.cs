using System.Diagnostics;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

using ScaleOutContracts;

namespace WorkDoer
{
    public class PickProcessor
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudQueueClient _queueClient;
        private readonly CloudQueue _queue;

        public PickProcessor()
        {
            _storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));
            
            _queueClient = _storageAccount.CreateCloudQueueClient();
        
            _queue = _queueClient.GetQueueReference("scaleoutsamplequeue");

            _queue.CreateIfNotExists();
        }

        public void ProcessPick()
        {
            CloudQueueMessage retrievedMessage = _queue.GetMessage();

            if (retrievedMessage != null)
            {
                Pick pick = new Pick(retrievedMessage.AsString);
                Trace.WriteLine(string.Format("Picked {0} doses for {1}.", pick.Quantity, pick.Item));
                _queue.DeleteMessage(retrievedMessage);
            }
        }
    }
}