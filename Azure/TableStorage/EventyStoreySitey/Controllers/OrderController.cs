using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using EventyStoreySitey.Models;
using EventyStoreySitey.Models.TableEventStore;

namespace EventyStoreySitey.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IAggregateRepository _repository = new AggregateRepository();

        public Order Get(string id)
        {
            return _repository.GetAggregate<Order>(id);
        }

        public HttpResponseMessage Post([FromBody] Customer customer)
        {
            var aggregateId = DateTimeOffset.UtcNow.ToString("yyyyMMddhhmmssffffff");
            var orderStarted = new OrderStarted
                                            {
                                                AggregateId = aggregateId, 
                                                CustomerName = customer.CustomerName
                                            };
            _repository.StoreEvent<Order>(orderStarted);

            var response = Request.CreateResponse(HttpStatusCode.Created);

            var uri = Url.Link("DefaultApi", new { id = aggregateId });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void Post(string id, [FromBody] Item item)
        {
            var itemAdded = new ItemAdded
                {
                    AggregateId = id, 
                    ItemName = item.ItemName, 
                    Quantity = item.Quantity
                };
            _repository.StoreEvent<Order>(itemAdded);
        }
    }
}