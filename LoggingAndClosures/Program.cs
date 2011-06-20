using System;
using System.Diagnostics;
using System.Threading;

namespace LoggingAndClosures
{
    class Program
    {
        static void Main(string[] args)
        {
            Stuff stuff = new Stuff {Value = "test value"};
            Logger logger = new Logger();

            Console.WriteLine("First Case: (Expect entry to be output)");
            logger.LoggingEnabled = true;
            logger.Log("Test");
            Console.WriteLine();

            Console.WriteLine("Second Case: (Expect entry to be output, with property read)");
            logger.LoggingEnabled = true;
            logger.Log(() => string.Format("Test {0}, {1}, {2}", stuff.Value, 2, 3));
            Console.WriteLine();

            Console.WriteLine("Third Case: (Expect no output, and property not read)");
            logger.LoggingEnabled = false;
            logger.Log(() => string.Format("Test {0}, {1}, {2}", stuff.Value, 3, 4));
            Console.WriteLine();

            Console.WriteLine("Fourth Case: (Delay Log twice changing property value in between)");
            logger.LoggingEnabled = true;
            logger.DelayLog(() => string.Format("Test {0}, {1}, {2}", stuff.Value, 3, 4));
            stuff.Value = "A New Value!";
            logger.DelayLog(() => string.Format("Test {0}, {1}, {2}", stuff.Value, 3, 4));
            Console.WriteLine();

            Console.WriteLine("Fifth Case: (Delay Log twice changing string variable value in between)");
            logger.LoggingEnabled = true;
            string testValue = "X";
            logger.DelayLog(() => string.Format("Test {0}, {1}, {2}", testValue, 3, 4));
            testValue = "Y";
            logger.DelayLog(() => string.Format("Test {0}, {1}, {2}", testValue, 3, 4));
            Console.WriteLine();

            Console.WriteLine("ALL DONE");
            Console.ReadLine();

            Console.WriteLine("\r\n\r\nSpeed test:");
            logger.LoggingEnabled = false;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            const int repetitions = 100;
            for (int i = 0; i < repetitions; i++)
            {
                logger.Log(string.Format("Test {0}, {1}, {2}", 1, 3, 4));
            }
            watch.Stop();
            long inline = watch.ElapsedTicks;
            watch.Reset();
            watch.Start();
            for (int i = 0; i < repetitions; i++)
            {
                logger.Log(() => string.Format("Test {0}, {1}, {2}", 1, 3, 4));
            }
            watch.Stop();
            long lambda = watch.ElapsedTicks;

            Console.WriteLine("Inline took {0} ticks", inline);
            Console.WriteLine("Lambda took {0} ticks", lambda);

            Console.WriteLine("ALL DONE Again");
            Console.ReadLine();
        }
    }

    public class Stuff
    {
        private string _value;

        public string Value
        {
            get
            {
                Console.WriteLine("In Stuff.get_Value");
                return _value;
            }
            set { _value = value; }
        }
    }

    public class Logger
    {
        public bool LoggingEnabled { get; set; }

        public void Log(string entry)
        {
            if (LoggingEnabled)
            {
                Console.WriteLine("Logged=>{0}", entry);
            }
        }

        public void Log(Func<string> entryCalculation)
        {
            if (LoggingEnabled)
            {
                Console.WriteLine("Logged=>{0}", entryCalculation());
            }
        }

        public void DelayLog(Func<string> entryCalculation)
        {
            if (LoggingEnabled)
            {
                ThreadPool.QueueUserWorkItem(Callback, entryCalculation);
            }
        }

        private void Callback(object calculation)
        {
            Thread.Sleep(1000);
            Func<string> entryCalculation = calculation as Func<string>;
            if (entryCalculation != null)
            {
                Console.WriteLine("Logged=>{0}", entryCalculation());
            }
        }
    }
}
