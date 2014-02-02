using System;

namespace Books.Models
{
    public class BookQuery
    {
        public DateTime? after { get; set; }

        public DateTime? before { get; set; }

        public bool IsMatch(Book book)
        {
            if (after != null)
            {
                if (book.PubDate < after)
                {
                    return false;
                }
            }

            if (before != null)
            {
                if (book.PubDate > before)
                {
                    return false;
                }
            }

            return true;
        }
    }
}