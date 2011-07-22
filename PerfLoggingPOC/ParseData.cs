using System;

namespace PerfLoggingPOC
{
    public class ParseData
    {
        private DateTime _dateInfo;
        private string _token;
        private string _action;

        public ParseData(string entry)
        {
            string[] parts = entry.Split('\t');
            _dateInfo = DateTime.Parse(parts[0]);
            _token = parts[1];
            _action = parts[3];
        }

        public string Token
        {
            get { return _token; }
        }

        public DateTime DateInfo
        {
            get { return _dateInfo; }
        }

        public string Action
        {
            get { return _action; }
        }
    }
}