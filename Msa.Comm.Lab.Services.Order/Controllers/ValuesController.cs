using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Msa.Comm.Lab.Services.Order.ApiClients;

namespace Msa.Comm.Lab.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ICatalogApiClient _catalogApiClient;

        public ValuesController(ICatalogApiClient catalogApiClient)
        {
            _catalogApiClient = catalogApiClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await _catalogApiClient.GetProductsAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "test";
        }
    }
}
