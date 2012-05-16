namespace ScheduleMaker
{
    class TimeSlot
    {
        public TimeSlot(string time)
        {
            Time = time;
        }

        public string Time { get; private set; }
    }
}