namespace ScheduleMaker
{
    internal class Presenter
    {
        public Presenter(string presenterFullName,
                         string presenterBio, 
                         string blog, 
                         string twitter)
        {
            Blog = blog;
            PresenterBio = presenterBio;
            PresenterFullName = presenterFullName;
            Twitter = twitter.Replace("@", string.Empty);
        }

        public string Blog { get; private set; }

        public string PresenterBio { get; private set; }

        public string PresenterFullName { get; private set; }

        public string Twitter { get; private set; }

        public string PresenterId
        {
            get
            {
                return PresenterFullName.Trim().Replace(" ", string.Empty).Replace(".", string.Empty).Replace("\"", string.Empty).ToLower();
            }
        }
    }
}