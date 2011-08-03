using System;

namespace ParsePerfLogPOC
{
    public class ActionDetailRecord
    {
        private readonly string _actionName;
        private readonly DateTime _actionStart;
        private readonly long _totalMilliseconds;

        public ActionDetailRecord(ParseData firstRecord, ParseData secondRecord)
        {
            DateTime firstTime;
            DateTime secondTime;
            if (firstRecord.DateInfo < secondRecord.DateInfo)
            {
                firstTime = firstRecord.DateInfo;
                secondTime = secondRecord.DateInfo;
            }
            else
            {
                firstTime = secondRecord.DateInfo;
                secondTime = firstRecord.DateInfo;
            }
            _actionStart = firstTime;
            _actionName = firstRecord.Action;
            _totalMilliseconds = (long) (secondTime - firstTime).TotalMilliseconds;
        }

        public DateTime ActionStart
        {
            get { return _actionStart; }
        }

        public string ActionName
        {
            get { return _actionName; }
        }

        public long TotalMilliseconds
        {
            get { return _totalMilliseconds; }
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1:yyyy-MM-dd HH:mm:ss.ffffff}\t{2}", 
                _actionName, 
                _actionStart, 
                _totalMilliseconds);
        }
    }
}