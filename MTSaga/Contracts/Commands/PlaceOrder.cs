using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Commands
{
    public interface PlaceOrder
    {
        Guid CommandId { get; }

        DateTimeOffset Timestamp { get; }

        Order NewOrder { get; }
    }
}
