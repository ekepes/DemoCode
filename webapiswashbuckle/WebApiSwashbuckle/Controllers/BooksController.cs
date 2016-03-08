using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiSwashbuckle.Models;

namespace WebApiSwashbuckle.Controllers
{
    /// <summary>
    /// The Books resource represents the collection of books contained
    /// in the library.
    /// </summary>
    [RoutePrefix("api/v1.0/values")]
    public class BooksController : ApiController
    {
        private static readonly List<Book> Books = new List<Book>
        {
            new Book(1, "Title a"),
            new Book(2, "Title b"),
        };

        /// <summary>
        /// Retrieve the entire catalog of books in the library.
        /// </summary>
        /// <returns>An list of all of the books.</returns>
        [HttpGet]
        [Route("")]
        public IEnumerable<Book> Get()
        {
            return Books;
        }

        /// <summary>
        /// Retrieve a single book, based on it's id.
        /// </summary>
        /// <param name="id">The integer identifying the book.</param>
        /// <returns>The proper book.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public Book Get(int id)
        {
            return Books.FirstOrDefault(b => b.Id == id);
        }

        /// <summary>
        /// Adds a new book to the library.
        /// </summary>
        /// <param name="book">The book to add.</param>
        [HttpPost]
        [Route("")]
        public void Post([FromBody]Book book)
        {
            Books.Add(book);
        }

        /// <summary>
        /// Replace a book in the library.
        /// </summary>
        /// <param name="id">The id of the book to replace.</param>
        /// <param name="book">The book that will replace the current one.</param>
        [HttpPut]
        [Route("{id:int}")]
        public void Put(int id, [FromBody]Book book)
        {
            var existingBook = Books.First(b => b.Id == id);
            Books.Remove(existingBook);
            Books.Add(book);
        }

        /// <summary>
        /// Remove a book from the library.
        /// </summary>
        /// <param name="id">The id of the book to remove.</param>
        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            var existingBook = Books.First(b => b.Id == id);
            Books.Remove(existingBook);
        }
    }
}
