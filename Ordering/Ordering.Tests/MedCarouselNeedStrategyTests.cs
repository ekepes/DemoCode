using NUnit.Framework;

namespace Ordering.Tests
{
    [TestFixture]
    public class MedCarouselNeedStrategyTests : StrategyTestBase
    {
        [Test]
        public void WhenThereIsNoNeed()
        {
            MedCarouselNeedStrategy strategy = new MedCarouselNeedStrategy();
            int need = strategy.DetermineNeed(GetInventoryLevelObject(10, 40, 10, 10));

            Assert.AreEqual(0, need);
        }

        [Test]
        public void WhenNeededQuantityIsEvenlyDivisibleByPackageSize()
        {
            MedCarouselNeedStrategy strategy = new MedCarouselNeedStrategy();
            int need = strategy.DetermineNeed(GetInventoryLevelObject(4, 40, 10, 6));

            Assert.AreEqual(6, need);
        }

        [Test]
        public void WhenNeededQuantityIsGreaterThanAnEvenlyDivisibleByPackageSize()
        {
            MedCarouselNeedStrategy strategy = new MedCarouselNeedStrategy();
            int need = strategy.DetermineNeed(GetInventoryLevelObject(4, 40, 10, 10));

            Assert.AreEqual(4, need);
        }
    }
}