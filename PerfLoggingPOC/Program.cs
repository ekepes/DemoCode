using System;

namespace PerfLoggingPOC
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Experiment experiment = new Experiment())
            {
                experiment.Execute();

                Console.WriteLine("Done - press enter...");
                Console.ReadLine();
            }
        }
    }
}
