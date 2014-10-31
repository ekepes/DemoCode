using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events
{
    public interface OrderSubmitted : CorrelatedBy<Guid>
    {
        Guid EventId { get; }
        DateTimeOffset Timestamp { get; }
        Order Order { get; }
    }
}
