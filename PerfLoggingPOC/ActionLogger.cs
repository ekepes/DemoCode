using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using PerfLoggingPOC.Properties;

namespace PerfLoggingPOC
{
    public class ActionLogger : IDisposable
    {
        private readonly bool _isLoggingEnabled = Settings.Default.EnablePerformanceLogging;
        private readonly Guid _emptyGuid = new Guid();

        private static readonly object _lock = new object();
        private readonly Thread _eventualLoggingThread;
        private readonly ConcurrentQueue<LogEntry> _loggingQueue = new ConcurrentQueue<LogEntry>();
        private readonly StreamWriter _writer;
        private bool _keepLogging;

        public ActionLogger()
        {
            if (_isLoggingEnabled)
            {
                _writer = new StreamWriter("E:\\perf.log", true);
                _writer.AutoFlush = true;

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

                if (_writer != null)
                {
                    _writer.Close();
                    _writer.Dispose();
                }
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
            LogEntry entry;
            while (_loggingQueue.TryDequeue(out entry))
            {
                _writer.WriteLine(entry);
            }
        }

        public Guid LogStart(string actionName)
        {
            if (_isLoggingEnabled)
            {
                LogEntry logEntry = new LogEntry(actionName);
                _loggingQueue.Enqueue(logEntry);
                return logEntry.Token;
            }
            return _emptyGuid;
        }

        public void LogEnd(Guid token, string actionName)
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

                Guid token = LogStart(actionName);

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