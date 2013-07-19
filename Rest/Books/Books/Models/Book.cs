using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Books.Models
{
    public class Book
    {
        public string Isbn { get; set; }

        public string Title { get; set; }

        public Author Author { get; set; }

        public int Pages { get; set; }

        public decimal Price { get; set; }
    }
}