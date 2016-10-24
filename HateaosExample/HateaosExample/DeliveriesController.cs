using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HateaosExample
{
    [RoutePrefix("api/deliveries")]
    public class DeliveriesController : ApiController
    {
        private readonly DeliveriesRepository _repository = new DeliveriesRepository();

        [HttpGet, Route("")]
        public IHttpActionResult Get()
        {
            var deliveries = _repository.GetDeliveries().Select(d =>
            {
                d.AddLinks(CreateLinks(d));
                return d;
            });

            return Ok(deliveries);
        }

        [HttpGet, Route("{id}", Name = "GetDeliveryById")]
        public IHttpActionResult Get(string id)
        {
            var delivery = _repository.GetDelivery(id);
            if (delivery == default(Delivery))
            {
                return NotFound();
            }
            delivery.AddLinks(CreateLinks(delivery));
            return Ok(delivery);
        }

        [HttpPut, Route("{id}/status", Name = "ChangeStatusById")]
        public IHttpActionResult ChangeStatus(string id, [FromUri]string status)
        {
            var delivery = _repository.GetDelivery(id);
            if (delivery == default(Delivery))
            {
                return NotFound();
            }
            delivery.ChangeStatus(status);
            return Ok();
        }

        private IEnumerable<Link> CreateLinks(Delivery delivery)
        {
            var links = new[]
            {
                new Link
                {
                    Method = "GET",
                    Rel = "self",
                    Href = Url.Link("GetDeliveryById", new {id = delivery.Id})
                },
                new Link
                {
                    Method = "PUT",
                    Rel = "status-delivered",
                    Href = Url.Link("ChangeStatusById", new {id = delivery.Id, status = "delivered"})
                }
            };

            return links;
        }
    }
}