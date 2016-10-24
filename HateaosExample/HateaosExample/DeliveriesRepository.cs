using System.Collections.Generic;

namespace HateaosExample
{
    public class DeliveriesRepository
    {
        private readonly List<Delivery> deliveries = new List<Delivery> {
            new Delivery
            {
                Id = "D001",
                Destination = "Someplace",
                HandlerId = "P01",
                Origin = "Another Place",
                Status = "Not Delivered"
            },
            new Delivery
            {
                Id = "D002",
                Destination = "Someplace",
                HandlerId = "P01",
                Origin = "Another Place",
                Status = "Not Delivered"
            },
            new Delivery
            {
                Id = "D003",
                Destination = "Someplace",
                HandlerId = "P01",
                Origin = "Another Place",
                Status = "Not Delivered"
            }
        };

        public IEnumerable<Delivery> GetDeliveries()
        {
            return deliveries;
        }

        public Delivery GetDelivery(string id)
        {
            return deliveries.Find(d => d.Id == id);
        }
    }
}