namespace Ordering
{
    public class MedCarouselNeedStrategy : IDetermineNeed
    {
        public int DetermineNeed(InventoryLevel inventoryLevel)
        {
            if (inventoryLevel.Quantity >= inventoryLevel.Par)
            {
                return 0;
            }

            int quantityNeeded = inventoryLevel.Maximum - inventoryLevel.Quantity;
            int packagesNeeded = quantityNeeded / inventoryLevel.PackageSize;
            if (quantityNeeded % inventoryLevel.PackageSize > 0)
            {
                packagesNeeded++;
            }

            return packagesNeeded;
        }
    }
}