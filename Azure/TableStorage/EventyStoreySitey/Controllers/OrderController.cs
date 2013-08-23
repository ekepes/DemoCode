using System.Collections.Generic;
using System.Web.Http;

using EventyStoreySitey.Models;

namespace EventyStoreySitey.Controllers
{
    public class OrderController : ApiController
    {
        public void Post([FromBody]string value)
        {
            var repository = new EventRepository();

            var itemAdded = new ItemAdded {AggregateId = "12345678", ItemName = value, Quantity = 5};
            repository.StoreEvent<Order>(itemAdded);
        }

        public IList<IDomainEvent> Get(string id)
        {
            var repository = new EventRepository();

            return repository.GetEvents<Order>(id);
        }
    }
}