using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace ScheduleMaker
{
    class Occasion
    {
        public Occasion(List<TimeSlot> times, List<Session> sessions, List<Presenter> presenters)
        {
            Times = times;
            Sessions = sessions;
            Presenters = presenters;
        }

        public List<TimeSlot> Times { get; private set; }
        public List<Session> Sessions { get; private set; }
        public List<Presenter> Presenters { get; private set; }
        
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Occasion occasion = GetOccasion();

            using (var fileStream = File.OpenWrite("D:\\temp\\sessions.html"))
            {
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.WriteLine("<html>");
                    writer.WriteLine("<body>");
                    foreach (var timeSlot in occasion.Times)
                    {
                        WriteElement(writer, "h3", timeSlot.Time);

                        List<Session> sessions = occasion.Sessions.FindAll(x => x.TimeSlot == timeSlot);
                        foreach (var session in sessions)
                        {
                            WriteElement(writer, "h3", session.SessionTitle);
                            var presenter = session.Presenter;
                            WriteElement(writer, "h4", "<a href=\"speakers.html#" + presenter.PresenterId + "\">" + presenter.PresenterFullName + "</a>");
                            WriteElement(writer, "p", session.Description);
                            WriteElement(writer, "h4", session.Room);
                            writer.WriteLine("<hr/>");
                        }
                    }
                    writer.WriteLine("</body>");
                    writer.WriteLine("</html>");
                }
            }

            using (var fileStream = File.OpenWrite("D:\\temp\\speakers.html"))
            {
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.WriteLine("<html>");
                    writer.WriteLine("<body>");
                    foreach (var presenter in occasion.Presenters)
                    {
                        WriteElement(writer, "h3", "id=\"" + presenter.PresenterId + "\"", presenter.PresenterFullName);
                        WriteElement(writer, "p", presenter.PresenterBio);
                        WriteElement(writer, "p", "Blog/Website: <a href=\"" + presenter.Blog + "\">" + presenter.Blog + "</a>");
                        WriteElement(writer, "p", "Twitter: <a href=\"http://twitter.com/" + presenter.Twitter + "\">@" + presenter.Twitter + "</a>");
                        writer.WriteLine("<hr/>");
                    }
                    writer.WriteLine("</body>");
                    writer.WriteLine("</html>");
                }
            }
        }

        private static void WriteElement(StreamWriter writer, string tag, string value)
        {
            WriteElement(writer, tag, string.Empty, value);
        }

        private static void WriteElement(StreamWriter writer, string tag, string attributes, string value)
        {
            writer.WriteLine("<{0} {1}>{2}</{0}>", tag, attributes, value);
        }

        private static Occasion GetOccasion()
        {
            List<TimeSlot> times = new List<TimeSlot>();
            List<Session> sessions = new List<Session>();
            List<Presenter> presenters = new List<Presenter>();

            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\kepeseri\\Downloads\\Pittsburgh TechFest 2012 Schedule.xls;Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";"))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM [Sheet1$]";
                    command.CommandType = CommandType.Text;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string time = reader[0].ToString();
                            string room = reader[1].ToString();
                            string sessionTitle = reader[2].ToString();
                            string description = reader[3].ToString();
                            string presenterFullName = reader[4].ToString();
                            string presenterBio = reader[5].ToString();
                            string twitter = reader[6].ToString();
                            string blog = reader[7].ToString();

                            TimeSlot slot = times.Find(x => x.Time == time);
                            if (slot == null)
                            {
                                slot = new TimeSlot(time);
                                times.Add(slot);
                            }

                            Presenter presenter = presenters.Find(x => x.PresenterFullName == presenterFullName);
                            if (presenter == null)
                            {
                                presenter = new Presenter(presenterFullName, presenterBio, blog, twitter);
                                presenters.Add(presenter);
                            }

                            Session session = new Session(sessionTitle, description, room, slot, presenter);
                            sessions.Add(session);
                        }
                    }
                }
            }

            return new Occasion(times, sessions, presenters);
        }
    }
}