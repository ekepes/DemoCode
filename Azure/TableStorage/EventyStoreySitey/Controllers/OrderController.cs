using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using EventyStoreySitey.Models;

namespace EventyStoreySitey.Controllers
{
    public class OrderController : ApiController
    {
        private readonly EventRepository _repository = new EventRepository();

        public Order Get(string id)
        {
            return _repository.GetAggregate<Order>(id);
        }

        public HttpResponseMessage Post([FromBody] Customer customer)
        {
            string aggregateId = DateTimeOffset.UtcNow.ToString("yyyyMMddhhmmssffffff");
            OrderStarted orderStarted = new OrderStarted
                                            {
                                                AggregateId = aggregateId, 
                                                CustomerName = customer.CustomerName
                                            };
            _repository.StoreEvent<Order>(orderStarted);

            var response = Request.CreateResponse(HttpStatusCode.Created);

            string uri = Url.Link("DefaultApi", new { id = aggregateId });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void Post(string id, [FromBody] Item item)
        {
            ItemAdded itemAdded = new ItemAdded { AggregateId = id, ItemName = item.ItemName, Quantity = item.Quantity };
            _repository.StoreEvent<Order>(itemAdded);
        }
    }
}