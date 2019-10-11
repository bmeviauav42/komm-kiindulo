using Msa.Comm.Lab.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Msa.Comm.Lab.Services.Order.IntegrationEvents
{
    public class OrderCreatedEvent : IOrderCreatedEvent
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
