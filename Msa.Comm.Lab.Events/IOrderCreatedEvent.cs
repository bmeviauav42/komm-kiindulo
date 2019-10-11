using System;

namespace Msa.Comm.Lab.Events
{
    public interface IOrderCreatedEvent
    {
        int ProductId { get; }
        int Quantity { get; }
    }
}
