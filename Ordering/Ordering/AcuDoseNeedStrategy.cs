namespace Ordering
{
    public class AcuDoseNeedStrategy : IDetermineNeed
    {
        public int DetermineNeed(InventoryLevel inventoryLevel)
        {
            if (inventoryLevel.Quantity > inventoryLevel.Par)
            {
                return 0;
            }

            int quantityNeeded = inventoryLevel.Maximum - inventoryLevel.Quantity;
            int packagesNeeded = quantityNeeded / inventoryLevel.PackageSize;

            return packagesNeeded;
        }
    }
}