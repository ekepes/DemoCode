using System;
using System.Collections.Concurrent;
using System.Threading;
using log4net;
using PerfLoggingPOC.Properties;

namespace PerfLoggingPOC
{
    public class ActionLogger4 : IDisposable
    {
        private readonly bool _isLoggingEnabled = Settings.Default.EnablePerformanceLogging;
        ILog _log = LogManager.GetLogger("PerformanceLogger");

        private static readonly object _lock = new object();
        private readonly Thread _eventualLoggingThread;
        private readonly ConcurrentQueue<LogEntry2> _loggingQueue = new ConcurrentQueue<LogEntry2>();
        private bool _keepLogging;

        public ActionLogger4()
        {
            if (_isLoggingEnabled)
            {
                _eventualLoggingThread = new Thread(DoActionLogging);
                lock (_lock)
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
                lock (_lock)
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
                lock (_lock)
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
            LogEntry2 entry;
            while (_loggingQueue.TryDequeue(out entry))
            {
                _log.Debug(entry);
            }
        }

        public long LogStart(string actionName)
        {
            if (_isLoggingEnabled)
            {
                LogEntry2 logEntry = new LogEntry2(actionName);
                _loggingQueue.Enqueue(logEntry);
                return logEntry.Token;
            }
            return 0;
        }

        public void LogEnd(long token, string actionName)
        {
            if (_isLoggingEnabled)
            {
                _loggingQueue.Enqueue(new LogEntry2(token, actionName));
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