namespace PerfLoggingPOC
{
    public class ActionSummaryRecord
    {
        private readonly string _actionName;
        private  int _executions;
        private  long _totalExecutionTime;

        public ActionSummaryRecord(string actionName)
        {
            _actionName = actionName;
            _executions = 0;
            _totalExecutionTime = 0;
        }

        public void AddExecution(long executionTime)
        {
            _executions++;
            _totalExecutionTime += executionTime;
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}",
                                 _actionName,
                                 _executions,
                                 _totalExecutionTime,
                                 _totalExecutionTime/_executions);
        }
    }
}