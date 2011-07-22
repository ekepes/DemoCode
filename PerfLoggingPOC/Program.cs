using System;

namespace PerfLoggingPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IExperiment experiment = new ExperimentThree())
            {
                experiment.Execute();

                Console.WriteLine("Done - press enter...");
                Console.ReadLine();
            }
        }
    }
}
