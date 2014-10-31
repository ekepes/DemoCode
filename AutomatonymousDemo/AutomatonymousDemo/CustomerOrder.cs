using Automatonymous;

namespace AutomatonymousDemo
{
    public class CustomerOrder
    {
        private readonly CustomerOrderStateMachine _stateMachine;

        public CustomerOrder(CustomerOrderStateMachine stateMachine,
            string customerName, 
            int itemsOrdered, 
            decimal amountOwed)
        {
            _stateMachine = stateMachine;
            CurrentState = stateMachine.Initial;

            CustomerName = customerName;
            ItemsOrdered = itemsOrdered;
            AmountOwed = amountOwed;
        }

        public State CurrentState { get; set; }

        public CompositeEventStatus PreparationStatus { get; set; }

        public string CustomerName { get; private set; }

        public int ItemsOrdered { get; private set; }

        public decimal AmountOwed { get; private set; }

        public int ItemsGathered { get; private set; }

        public decimal AmountPaid { get; private set; }

        public void Pay(decimal amountToPay)
        {
            AmountPaid += amountToPay;
            if (AmountPaid >= AmountOwed)
            {
                _stateMachine.RaiseEvent(this, _stateMachine.OrderPaid);
            }
        }

        public void Gather(int gatheredAmount)
        {
            ItemsGathered += gatheredAmount;
        }
    }
}
