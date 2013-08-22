using System.Collections.Generic;

namespace EventyStoreySitey.Models
{
    public class Order : IAggregate
    {
        public IList<Item> Items { get; set; }
    }
}