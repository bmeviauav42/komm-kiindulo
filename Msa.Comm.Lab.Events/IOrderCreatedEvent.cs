using System;

namespace Msa.Comm.Lab.Events
{
    public interface IOrderCreatedEvent
    {
        int ProductId { get; }
        DateTimeOffset OrderPlaced { get; }
        int Quantity { get; }
    }
}
