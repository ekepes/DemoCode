using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace Ordering.Tests
{
    [TestFixture]
    public class NeedStrategyFactoryTests
    {
        [Test]
        public void ReturnsAcuDoseStrategyForAcuDose()
        {
            Type strategyType = NeedStrategyFactory.GetStrategy("AcuDose").GetType();

            Assert.AreEqual(typeof(AcuDoseNeedStrategy), strategyType);
        }

        [Test]
        public void ReturnsAcuDoseStrategyForACUDOSE()
        {
            Type strategyType = NeedStrategyFactory.GetStrategy("ACUDOSE").GetType();

            Assert.AreEqual(typeof(AcuDoseNeedStrategy), strategyType);
        }

        [Test]
        public void ReturnsMedCarouselStrategyForMedCarousel()
        {
            Type strategyType = NeedStrategyFactory.GetStrategy("MedCarousel").GetType();

            Assert.AreEqual(typeof(MedCarouselNeedStrategy), strategyType);
        }

        [Test]
        public void ReturnsMedCarouselStrategyForMEDCarousel()
        {
            Type strategyType = NeedStrategyFactory.GetStrategy("MEDCarousel").GetType();

            Assert.AreEqual(typeof(MedCarouselNeedStrategy), strategyType);
        }

        [Test]
        public void ThrowsExceptionWhenBadDeviceTypePassed()
        {
            try
            {
                NeedStrategyFactory.GetStrategy("BadDeviceType");
            }
            catch (KeyNotFoundException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }
    }
}