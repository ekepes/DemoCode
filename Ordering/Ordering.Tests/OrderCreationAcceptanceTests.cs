using NUnit.Framework;

namespace Ordering.Tests
{
    [TestFixture]
    public class OrderCreationAcceptanceTests
    {
        [Test]
        public void VerifyHospitalOneOrder()
        {
            HospitalState state = new HospitalState();
            state.AddItem(new InventoryLevel("DOSE0001", "AcuDose", "Acetaminophen", 2, 20, 4, 6));
            state.AddItem(new InventoryLevel("MEDCAR01", "MedCarousel", "Bismuth Subsalicylate", 4, 40, 10, 10));

            OrderCreator creator = new OrderCreator();

            HospitalOrder order = creator.CreateOrder(state);

            Assert.Contains(new HospitalOrderItem("DOSE0001", "Acetaminophen", 3), order.Items);
            Assert.Contains(new HospitalOrderItem("MEDCAR01", "Bismuth Subsalicylate", 4), order.Items);
        }
    }
}