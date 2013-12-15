using NUnit.Framework;
using ProbabilityKata;

namespace ProbabilityKataTests
{
    [TestFixture]
    public class ProbabilityTests
    {
        [Test]
        public void Half_combined_with_half_is_quarter()
        {
            var expected = new Probability(0.25m);
            var actual = new Probability(0.5m).CombinedWith(new Probability(0.5m));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Inverse_of_quarter_is_three_quarters()
        {
            var expected = new Probability(0.75m);
            var actual = new Probability(0.25m).InverseOf();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Either_half_or_half_is_three_quarters()
        {
            var expected = new Probability(0.75m);
            var actual = new Probability(0.5m).Either(new Probability(0.5m));

            Assert.AreEqual(expected, actual);
        }
    }
}
