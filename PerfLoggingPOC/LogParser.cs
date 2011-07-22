using System.Collections.Generic;
using System.IO;

namespace PerfLoggingPOC
{
    public class LogParser
    {
        public void Parse(string fileName)
        {
            var pendingEntries = new Dictionary<string, ParseData>();
            List<ActionDetailRecord> details = new List<ActionDetailRecord>();
            using (var reader = new StreamReader(fileName))
            {
                while (reader.Peek() > 0)
                {
                    var data = new ParseData(reader.ReadLine());
                    string token = data.Token;
                    if (pendingEntries.ContainsKey(token))
                    {
                        ParseData firstEntry = pendingEntries[token];
                        details.Add(new ActionDetailRecord(firstEntry, data));
                        pendingEntries.Remove(token);
                    }
                    else
                    {
                        pendingEntries.Add(token, data);
                    }
                }
            }

            using (StreamWriter writer = new StreamWriter(Path.ChangeExtension(fileName, "detail.log"), false))
            {
                foreach (var actionDetailRecord in details)
                {
                    writer.WriteLine(actionDetailRecord);
                }
            }

            Dictionary<string, ActionSummaryRecord> summaries = new Dictionary<string, ActionSummaryRecord>();
            foreach (var actionDetailRecord in details)
            {
                ActionSummaryRecord summary;
                string actionName = actionDetailRecord.ActionName;
                if (!summaries.ContainsKey(actionName))
                {
                    summary = new ActionSummaryRecord(actionName);
                    summaries.Add(actionName, summary);
                }
                else
                {
                    summary = summaries[actionName];
                }

                summary.AddExecution(actionDetailRecord.TotalMilliseconds);
            }

            using (StreamWriter writer = new StreamWriter(Path.ChangeExtension(fileName, "summary.log"), false))
            {
                foreach (var actionSummaryRecord in summaries)
                {
                    writer.WriteLine(actionSummaryRecord.Value);
                }
            }
        }
    }
}