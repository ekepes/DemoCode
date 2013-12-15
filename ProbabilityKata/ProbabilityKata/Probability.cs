
namespace ProbabilityKata
{
    public class Probability
    {
        private readonly decimal _probability;

        public Probability(decimal probability)
        {
            _probability = probability;
        }

        public Probability CombinedWith(Probability probability)
        {
            return new Probability(_probability * probability._probability);
        }

        protected bool Equals(Probability other)
        {
            return _probability == other._probability;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Probability) obj);
        }

        public override int GetHashCode()
        {
            return _probability.GetHashCode();
        }

        public Probability InverseOf()
        {
            return new Probability(1m - _probability);
        }

        public Probability Either(Probability probability)
        {
            return new Probability(_probability + probability._probability - CombinedWith(probability)._probability);
        }

        public static Probability operator +(Probability p1, Probability p2)
        {
            return new Probability(p1._probability + p2._probability);
        }
    }
}
