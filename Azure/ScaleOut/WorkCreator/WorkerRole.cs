using System.Net;
using System.Threading;
using Microsoft.WindowsAzure.ServiceRuntime;

using ScaleOutContracts;

namespace WorkCreator
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            PickCreator creator = new PickCreator();

            while (true)
            {
                Thread.Sleep(TimingConstants.CreateIntervalInMilliseconds);

                creator.CreatePick();
            }
        }

        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 12;

            return base.OnStart();
        }
    }
}
