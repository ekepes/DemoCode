namespace ScaleOutContracts
{
    public class Pick
    {
        public Pick()
        {
        }

        public Pick(string serializedPickString)
        {
            string[] segments = serializedPickString.Split('|');
            Item = segments[0];
            Quantity = int.Parse(segments[1]);
        }

        public Pick(string item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public string Item { get; set; }

        public int Quantity { get; set; }

        public string SerializedPickString()
        {
            return string.Format("{0}|{1}", Item, Quantity);
        }
    }
}