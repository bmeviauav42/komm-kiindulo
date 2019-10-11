using MassTransit;
using Msa.Comm.Lab.Events;
using Msa.Comm.Lab.Services.Catalog.Controllers;
using Msa.Comm.Lab.Services.Catalog.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Msa.Comm.Lab.Services.Catalog.IntegrationEventHandlers
{
    public class OrderCreatedEventHandler : IConsumer<IOrderCreatedEvent>
    {
        public Task Consume(ConsumeContext<IOrderCreatedEvent> context)
        {
            var product = ProductController.Products.SingleOrDefault(p => p.ProductId == context.Message.ProductId);
            if (product != null)
            {
                product.Stock -= context.Message.Quantity;
            }

            return Task.CompletedTask;
        }
    }
}
