namespace ScheduleMaker
{
    internal class Session
    {
        public Session(string sessionTitle, 
                       string description, 
                       string room, 
                       TimeSlot timeSlot, 
                       Presenter presenter)
        {
            Description = description;
            if (room.ToLower().Contains("room"))
            {
                Room = room;
            }
            else
            {
                Room = "Room " + room;
            }
            SessionTitle = sessionTitle;
            TimeSlot = timeSlot;
            Presenter = presenter;
        }

        public string Description { get; private set; }

        public string Room { get; private set; }

        public string SessionTitle { get; private set; }

        public TimeSlot TimeSlot { get; private set; }

        public Presenter Presenter { get; private set; }
    }
}