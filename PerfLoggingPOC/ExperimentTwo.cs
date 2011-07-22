using System;
using System.Threading;

namespace PerfLoggingPOC
{
    public class ExperimentTwo : IExperiment
    {
        readonly ActionLogger _logger = new ActionLogger();
        readonly Random _random = new Random();

        public void Execute()
        {
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(DoWork, i);
            }
        }

        private void DoWork(object actionNumber)
        {
            _logger.LogAction("SampleAction", 
                () => Thread.Sleep(_random.Next(100)));
        }

        public void Dispose()
        {
            if (_logger != null)
            {
                _logger.Dispose();
            }
        }
    }
}