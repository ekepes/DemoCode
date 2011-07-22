namespace PerfLoggingPOC
{
    public class Sequence
    {
        private static readonly object _lock = new object();
        private long _nextValue;

        public Sequence()
        {
            _nextValue = 1;
        }

        public long NextValue
        {
            get
            {
                lock (_lock)
                {
                    return _nextValue++;
                }
            }
        }

    }
}