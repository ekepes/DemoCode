using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ordering
{
    public class HospitalState
    {
        private readonly List<InventoryLevel> _inventoryLevels = new List<InventoryLevel>();

        public ReadOnlyCollection<InventoryLevel> InventoryLevels
        {
            get
            {
                return _inventoryLevels.AsReadOnly();
            }
        }

        public void AddItem(InventoryLevel level)
        {
            _inventoryLevels.Add(level);
        }
    }
}