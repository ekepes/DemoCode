using System;

namespace InterfaceInheritenceExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            var saga = new SagaImplementation
            {
                CorrelationId = Guid.NewGuid(), 
                Name = "MySaga"
            };

            var correlatedBy = (CorrelatedBy<Guid>) saga;
            var sagaInstance = (SagaInstance) saga;

            Console.WriteLine("Straight up correlation id: {0}", saga.CorrelationId);
            Console.WriteLine("Correlation id via CorrelatedBy: {0}", correlatedBy.CorrelationId);
            Console.WriteLine("Correlation id via SagaInstance: {0}", sagaInstance.CorrelationId);

            Console.ReadLine();
        }
    }

    public interface CorrelatedBy<TKey>
    {
        TKey CorrelationId { get; }
    }

    public interface SagaInstance : CorrelatedBy<Guid>
    {
        string Name { get; set; }

        new Guid CorrelationId { get; set; }
    }

    public class SagaImplementation : SagaInstance
    {
        public string Name { get; set; }

        public Guid CorrelationId { get; set; }
    }
}
