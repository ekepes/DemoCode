namespace WebApiSwashbuckle.Models
{
    public class Book
    {
        public Book() {}

        public Book(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }

        public string Title { get; set; }
    }
}