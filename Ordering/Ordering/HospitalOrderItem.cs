namespace Ordering
{
    public class HospitalOrderItem
    {
        private readonly string _device;

        private readonly string _medication;

        private readonly int _quantity;

        protected bool Equals(HospitalOrderItem other)
        {
            return string.Equals(_device, other._device) && string.Equals(_medication, other._medication) && _quantity == other._quantity;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            var other = obj as HospitalOrderItem;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = _device.GetHashCode();
                hashCode = (hashCode * 397) ^ _medication.GetHashCode();
                hashCode = (hashCode * 397) ^ _quantity;
                return hashCode;
            }
        }

        public HospitalOrderItem(string device, 
            string medication, 
            int quantity)
        {
            _device = device;
            _medication = medication;
            _quantity = quantity;
        }

        public string Device
        {
            get
            {
                return _device;
            }
        }

        public string Medication
        {
            get
            {
                return _medication;
            }
        }

        public int Quantity
        {
            get
            {
                return _quantity;
            }
        }
    }
}