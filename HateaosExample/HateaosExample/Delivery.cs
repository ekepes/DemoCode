namespace HateaosExample
{
    public class Delivery : HateaosResource
    {
        public string Id { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public string Status { get; set; }

        public string HandlerId { get; set; }

        public void ChangeStatus(string newStatus)
        {
            Status = newStatus;
        }
    }
}