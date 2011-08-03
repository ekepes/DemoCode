using System;
using System.Diagnostics;

namespace PerfLoggingPOC
{
    public class Experiment : IDisposable
    {
        readonly ActionLogger _logger = new ActionLogger();

        public void Execute()
        {
            for (int loops = 0; loops < 30; loops++)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                for (int i = 0; i < 1000; i++)
                {
                    _logger.LogAction("SampleAction",
                                      DoWork);
                }
                watch.Stop();
                Console.WriteLine("{0} {1}", watch.ElapsedMilliseconds, watch.ElapsedTicks);
            }
        }

        private static void DoWork()
        {
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                count++;
            }
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