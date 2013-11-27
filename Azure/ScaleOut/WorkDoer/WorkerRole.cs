using System.Net;
using System.Threading;

using Microsoft.WindowsAzure.ServiceRuntime;

using ScaleOutContracts;

namespace WorkDoer
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            PickProcessor processor = new PickProcessor();

            while (true)
            {
                Thread.Sleep(TimingConstants.ProcessIntervalInMilliseconds);

                processor.ProcessPick();
            }
        }

        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 12;

            return base.OnStart();
        }
    }
}
