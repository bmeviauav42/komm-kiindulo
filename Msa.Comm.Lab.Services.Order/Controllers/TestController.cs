using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Msa.Comm.Lab.Services.Catalog.Grpc;
using Msa.Comm.Lab.Services.Order.ApiClients;
using Msa.Comm.Lab.Services.Order.IntegrationEvents;

namespace Msa.Comm.Lab.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //private readonly ICatalogApiClient _catalogApiClient;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly CatalogService.CatalogServiceClient _catalogServiceClient;

        public TestController(ICatalogApiClient catalogApiClient, IPublishEndpoint publishEndpoint, CatalogService.CatalogServiceClient catalogServiceClient)
        {
            //_catalogApiClient = catalogApiClient;
            _publishEndpoint = publishEndpoint;
            _catalogServiceClient = catalogServiceClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Catalog.Grpc.Product>>> Get()
        {
            return (await _catalogServiceClient.GetProductsAsync(new Empty())).Products;
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
