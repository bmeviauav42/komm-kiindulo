using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Msa.Comm.Lab.Services.Order.ApiClients;
using Msa.Comm.Lab.Services.Order.IntegrationEvents;

namespace Msa.Comm.Lab.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ICatalogApiClient _catalogApiClient;
        private readonly IPublishEndpoint _publishEndpoint;

        public TestController(ICatalogApiClient catalogApiClient, IPublishEndpoint publishEndpoint)
        {
            _catalogApiClient = catalogApiClient;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await _catalogApiClient.GetProductsAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return $"test: {id}";
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> CreateOrder()
        {
            await _publishEndpoint.Publish(new OrderCreatedEvent
            { 
                ProductId = 1,
                Quantity = 1,
                OrderPlaced = DateTimeOffset.UtcNow
            });

            return Ok(new { Message = "Megrendelés sikeres!" });
        }
    }
}
