using Automatonymous;
using Automatonymous.Graphing;
using Automatonymous.Visualizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatonymousDemo
{
    public class Program
    {
        static CustomerOrder _instance;
        static CustomerOrderStateMachine _machine;

        static void Main(string[] args)
        {
            _machine = new CustomerOrderStateMachine();
            _instance = _machine.CreateOrder("John Smith", 5, 19.99m);
            Console.WriteLine("Initial, State now = {0}", _instance.CurrentState.Name);

            FireEvent(_machine.OrderPlaced);

            _machine.RaiseEvent(_instance, _machine.PaymentReceived, 10m);
            LogEvent(_machine.PaymentReceived);
            _machine.RaiseEvent(_instance, _machine.PaymentReceived, 9.99m);
            LogEvent(_machine.PaymentReceived);

            FireEvent(_machine.OrderGathered);
            FireEvent(_machine.OrderPacked);
            FireEvent(_machine.OrderShipped);
            FireEvent(_machine.OrderClosed);
            
            CreateGraph();

            Console.WriteLine();
            Console.WriteLine("Done.");
            Console.ReadLine();
        }

        private static void FireEvent(Event eventToFire)
        {
            _machine.RaiseEvent(_instance, eventToFire);
            LogEvent(eventToFire);
        }

        private static void LogEvent(Event eventToFire)
        {
            Console.WriteLine("{0}, State now = {1}",
                eventToFire.Name,
                _instance.CurrentState.Name);
        }

        private static void CreateGraph()
        {
            StateMachineGraph graph = _machine.GetGraph();
            var generator = new StateMachineGraphvizGenerator(graph);
            var dots = generator.CreateDotFile();
        }
    }        
}