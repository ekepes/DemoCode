namespace ParsePerfLogPOC
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var parser = new LogParser();
            parser.Parse("E:\\perf.log");
        }
    }
}