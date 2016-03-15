using System;
using System.Runtime.CompilerServices;
using Serilog;

namespace SerilogPoc
{
    // Good stuff here: http://blachniet.com/blog/serilog-good-habits/
    class Program
    {
        static void Main(string[] args)
        {
            string logFormat = "{Timestamp} [{Level}] ({ThreadId}) {SourceContext:l} {Message}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithThreadId()
                .Enrich.WithMachineName()
                .WriteTo.ColoredConsole(outputTemplate:
        logFormat)
                .WriteTo.RollingFile(@"C:\tmp\Log-{Date}.txt", outputTemplate:
        logFormat)
                .MinimumLevel.Debug()
                .CreateLogger();

            var log = Log.ForContext<Program>();

            log.Information("Hello, Serilog!");

            log.Debug("Debug");
            log.Information("Info");
            log.Warning("Warning");
            log.Error("Error");

            Console.ReadLine();
        }
    }
}
