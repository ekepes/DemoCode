using System;
using System.Collections.Concurrent;
using System.Threading;
using log4net;
using PerfLoggingPOC.Properties;

namespace PerfLoggingPOC
{
    public class ActionLogger : IDisposable
    {
        private readonly bool _isLoggingEnabled = Settings.Default.EnablePerformanceLogging;
        readonly ILog _log = LogManager.GetLogger("PerformanceLogger");

        private static readonly object Lock = new object();
        private readonly Thread _eventualLoggingThread;
        private readonly ConcurrentQueue<LogEntry> _loggingQueue = new ConcurrentQueue<LogEntry>();
        private bool _keepLogging;

        public ActionLogger()
        {
            if (_isLoggingEnabled)
            {
                _eventualLoggingThread = new Thread(DoActionLogging);
                lock (Lock)
                {
                    _keepLogging = true;
                }
                _eventualLoggingThread.Start();
            }
        }

        public void Dispose()
        {
            if (_isLoggingEnabled)
            {
                lock (Lock)
                {
                    _keepLogging = false;
                }

                WriteOutEntries();
            }
        }

        private void DoActionLogging(object ignored)
        {
            while (true)
            {
                WriteOutEntries();

                Thread.Sleep(50);
                lock (Lock)
                {
                    if (!_keepLogging)
                    {
                        break;
                    }
                }
            }
        }

        private void WriteOutEntries()
        {
            LogEntry entry;
            while (_loggingQueue.TryDequeue(out entry))
            {
                _log.Debug(entry);
            }
        }

        public long LogStart(string actionName)
        {
            if (_isLoggingEnabled)
            {
                LogEntry logEntry = new LogEntry(actionName);
                _loggingQueue.Enqueue(logEntry);
                return logEntry.Token;
            }
            return 0;
        }

        public void LogEnd(long token, string actionName)
        {
            if (_isLoggingEnabled)
            {
                _loggingQueue.Enqueue(new LogEntry(token, actionName));
            }
        }

        public void LogAction(string actionName, Action action)
        {
            if (_isLoggingEnabled)
            {

                long token = LogStart(actionName);

                action();

                LogEnd(token, actionName);
            }
            else
            {
                action();
            }
        }
    }
}