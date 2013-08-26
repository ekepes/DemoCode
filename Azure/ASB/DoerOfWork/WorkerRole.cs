using System;
using System.Diagnostics;
using System.Net;
using System.Threading;

using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace DoerOfWork
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("DoerOfWork entry point called", "Information");

            string connectionString =
    CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

            QueueClient client =
                QueueClient.CreateFromConnectionString(connectionString, "TestQueue");

            while (true)
            {
                Thread.Sleep(1000);
                Trace.TraceInformation("Working", "Information");

                BrokeredMessage message = client.Receive();

                if (message != null)
                {
                    try
                    {
                        Console.WriteLine("Body: " + message.GetBody<string>());
                        Console.WriteLine("MessageID: " + message.MessageId);
                        Console.WriteLine("Test Property: " +
                           message.Properties["TestProperty"]);

                        // Remove message from queue
                        message.Complete();
                    }
                    catch (Exception)
                    {
                        // Indicate a problem, unlock message in queue
                        message.Abandon();
                    }
                }
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
