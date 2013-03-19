namespace Ordering
{
    public class InventoryLevel
    {
        private readonly string _device;

        private readonly string _deviceType;

        private readonly int _maximum;

        private readonly string _medication;

        private readonly int _packageSize;

        private readonly int _par;

        private readonly int _quantity;

        public InventoryLevel(
            string device, 
            string deviceType, 
            string medication, 
            int quantity, 
            int maximum, 
            int par, 
            int packageSize)
        {
            _device = device;
            _deviceType = deviceType;
            _medication = medication;
            _quantity = quantity;
            _maximum = maximum;
            _par = par;
            _packageSize = packageSize;
        }

        public string Device
        {
            get
            {
                return _device;
            }
        }

        public string DeviceType
        {
            get
            {
                return _deviceType;
            }
        }

        public int Maximum
        {
            get
            {
                return _maximum;
            }
        }

        public string Medication
        {
            get
            {
                return _medication;
            }
        }

        public int PackageSize
        {
            get
            {
                return _packageSize;
            }
        }

        public int Par
        {
            get
            {
                return _par;
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