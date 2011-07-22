using System;
using System.Threading;

namespace PerfLoggingPOC
{
    public class LogEntry
    {
        private readonly string _entry;
        private readonly DateTime _entryDate;
        private readonly int _threadId;
        private readonly Guid _token;

        public Guid Token
        {
            get { return _token; }
        }

        public LogEntry(string entry)
        {
            _entryDate = DateTime.Now;
            _threadId = Thread.CurrentThread.ManagedThreadId;
            _entry = entry;
            _token = Guid.NewGuid();
        }

        public LogEntry(Guid token, string entry)
        {
            _entryDate = DateTime.Now;
            _threadId = Thread.CurrentThread.ManagedThreadId;
            _entry = entry;
            _token = token;
        }

        public override string ToString()
        {
            return string.Format("{0:yyyy-MM-dd HH:mm:ss.ffffff}\t{1}\t[{2}]\t{3}",
                                 _entryDate,
                                 _token,
                                 _threadId,
                                 _entry);
        }
    }
}