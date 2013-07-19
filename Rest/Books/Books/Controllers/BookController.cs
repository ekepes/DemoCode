using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Books.Models;

namespace Books.Controllers
{
    public class BookController : ApiController
    {
        public static readonly List<Book> Books = new List<Book>
                            {
                                new Book
                                    {
                                        Isbn = "123-456-789",
                                        Title = "Crappy crap about crap",
                                        Price = (decimal)19.99,
                                        Pages = 666,
                                        Author = new Author { FirstName = "John", LastName = "Crapper" }
                                    }
                            };


        public IEnumerable<Book> Get()
        {
            return Books;
        }

        public HttpResponseMessage Get(string id)
        {
            var foundBook = Books.Find(book => book.Isbn == id);

            if (foundBook == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, 
                    string.Format("ISBN=[{0}] does not exist.", id));
            }

            return Request.CreateResponse(HttpStatusCode.OK, foundBook);
        }

        public HttpResponseMessage Post(Book book)
        {
            Books.Add(book);

            var response = Request.CreateResponse(HttpStatusCode.Created, book);

            string uri = Url.Link("DefaultApi", new { id = book.Isbn });
            response.Headers.Location = new Uri(uri);
            return response;
        }
    }
}