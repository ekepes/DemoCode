using NUnit.Framework;

namespace Ordering.Tests
{
    [TestFixture]
    public class AcuDoseNeedStrategyTests : StrategyTestBase
    {
        [Test]
        public void WhenThereIsNoNeed()
        {
            AcuDoseNeedStrategy strategy = new AcuDoseNeedStrategy();
            int need = strategy.DetermineNeed(GetInventoryLevelObject(5, 20, 4, 6));

            Assert.AreEqual(0, need);
        }

        [Test]
        public void WhenNeededQuantityIsEvenlyDivisibleByPackageSize()
        {
            AcuDoseNeedStrategy strategy = new AcuDoseNeedStrategy();
            int need = strategy.DetermineNeed(GetInventoryLevelObject(2, 20, 4, 6));

            Assert.AreEqual(3, need);
        }

        [Test]
        public void WhenNeededQuantityIsGreaterThanAnEvenlyDivisibleByPackageSize()
        {
            AcuDoseNeedStrategy strategy = new AcuDoseNeedStrategy();
            int need = strategy.DetermineNeed(GetInventoryLevelObject(4, 20, 4, 6));

            Assert.AreEqual(2, need);
        }
    }
}