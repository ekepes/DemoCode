﻿using System;
using System.Collections.Generic;
using System.Linq;
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
                                        PubDate = new DateTime(2001, 1, 1),
                                        Author = new Author { FirstName = "John", LastName = "Crapper" }
                                    },
                                new Book
                                    {
                                        Isbn = "456-789-123",
                                        Title = "Stuffy stuff about stuff",
                                        Price = (decimal)29.99,
                                        Pages = 999,
                                        PubDate = new DateTime(2011, 1, 1),
                                        Author = new Author { FirstName = "John", LastName = "Crapper" }
                                    },
                                new Book
                                    {
                                        Isbn = "789-123-456",
                                        Title = "Fishy fish about fish",
                                        Price = (decimal)39.99,
                                        Pages = 333,
                                        PubDate = new DateTime(1991, 1, 1),
                                        Author = new Author { FirstName = "John", LastName = "Crapper" }
                                    }
                            };

        public HttpResponseMessage Get([FromUri] BookQuery query)
        {
            var books = Books.Where(query.IsMatch).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, books);
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