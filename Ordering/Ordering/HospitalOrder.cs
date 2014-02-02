using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ordering
{
    public class HospitalOrder
    {
        private readonly List<HospitalOrderItem> _items = new List<HospitalOrderItem>();

        public ReadOnlyCollection<HospitalOrderItem> Items
        {
            get
            {
                return _items.AsReadOnly();
            }
        }

        public void AddItem(HospitalOrderItem hospitalOrderItem)
        {
            _items.Add(hospitalOrderItem);
        }
    }
}