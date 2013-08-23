using System.Collections.Generic;
using System.Web.Http;

using EventyStoreySitey.Models;

namespace EventyStoreySitey.Controllers
{
    public class OrderController : ApiController
    {
        public void Post(string id, [FromBody]Item item)
        {
            var repository = new EventRepository();

            var itemAdded = new ItemAdded
                {
                    AggregateId = id, 
                    ItemName = item.ItemName, 
                    Quantity = item.Quantity
                };
            repository.StoreEvent<Order>(itemAdded);
        }

        public Order Get(string id)
        {
            var repository = new EventRepository();

            return repository.GetAggregate<Order>(id);
        }
    }
}