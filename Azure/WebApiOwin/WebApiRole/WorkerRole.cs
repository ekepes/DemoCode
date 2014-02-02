using Microsoft.Owin.Hosting;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace WebApiRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private IDisposable _webApp = null;

        public override void Run()
        {
            Trace.TraceInformation("WebApiRole entry point called", "Information");

            while (true)
            {
                Thread.Sleep(10000);
                Trace.TraceInformation("Working", "Information");
            }
        }

        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 12;

            var endpoint = RoleEnvironment.CurrentRoleInstance.InstanceEndpoints["Endpoint1"];
            string baseUri = string.Format("{0}://{1}",
                endpoint.Protocol, endpoint.IPEndpoint);

            Trace.TraceInformation(String.Format("Starting OWIN at {0}", baseUri),
                "Information");

            _webApp = WebApp.Start<Startup>(new StartOptions(baseUri));

            return base.OnStart();
        }

        public override void OnStop()
        {
            if (_webApp != null)
            {
                _webApp.Dispose();
            }
            base.OnStop();
        }
    }
}