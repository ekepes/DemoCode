using System;

namespace PerfLoggingPOC
{
    public interface IExperiment : IDisposable
    {
        void Execute();
    }
}