using System.Collections.Generic;

namespace Ordering
{
    public static class NeedStrategyFactory
    {
        private static readonly Dictionary<string, IDetermineNeed> Strategies
            = new Dictionary<string, IDetermineNeed>
            {
                {
                    "acudose",
                    new AcuDoseNeedStrategy()
                },
                {
                    "medcarousel",
                    new MedCarouselNeedStrategy()
                }
            };

        public static IDetermineNeed GetStrategy(string deviceType)
        {
            return Strategies[deviceType.ToLower()];
        }
    }
}