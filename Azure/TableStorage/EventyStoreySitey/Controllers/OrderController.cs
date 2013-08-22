using System.Web.Http;

using EventyStoreySitey.Models;

namespace EventyStoreySitey.Controllers
{
    public class OrderController : ApiController
    {
        public void Post([FromBody]string value)
        {
            EventRepository repository = new EventRepository();

            repository.StoreEvent(new ItemAdded { AggregateId = "12345678", ItemName = value, Quantity = 5 });
        }
    }
}