namespace Ordering
{
    public class OrderCreator
    {
        public HospitalOrder CreateOrder(HospitalState state)
        {
            HospitalOrder order = new HospitalOrder();

            foreach (var inventoryLevel in state.InventoryLevels)
            {
                IDetermineNeed needStrategy = NeedStrategyFactory.GetStrategy(inventoryLevel.DeviceType);
                int packagesNeeded = needStrategy.DetermineNeed(inventoryLevel);
                if (packagesNeeded > 0)
                {
                    order.AddItem(
                        new HospitalOrderItem(inventoryLevel.Device, inventoryLevel.Medication, packagesNeeded));
                }
            }

            return order;
        }
    }
}