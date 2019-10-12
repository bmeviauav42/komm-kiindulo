using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Msa.Comm.Lab.Services.Catalog.Exceptions;
using Msa.Comm.Lab.Services.Catalog.Models;

namespace Msa.Comm.Lab.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        internal static List<Product> _products = new List<Product>
        {
            new Product { ProductId = 1, Name = "Sör", Stock = 10, UnitPrice = 250 },
            new Product { ProductId = 2, Name = "Bor", Stock = 5, UnitPrice = 890 },
            new Product { ProductId = 3, Name = "Csoki", Stock = 15, UnitPrice = 200 },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var rand = new Random();
            if (rand.Next() % 5 == 0)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
            else
            {
                return _products;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _products.SingleOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return product;
            }
        }
    }
}
