namespace Ordering.Tests
{
    public abstract class StrategyTestBase
    {
        protected InventoryLevel GetInventoryLevelObject(int quantity,
                                                         int maximum,
                                                         int par,
                                                         int packageSize)
        {
            return new InventoryLevel(string.Empty,
                string.Empty,
                string.Empty,
                quantity,
                maximum,
                par,
                packageSize);
        }
    }
}