using Grpc.Core;
using Microsoft.Extensions.Logging;
using Msa.Comm.Lab.Services.Catalog.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Msa.Comm.Lab.Services.Catalog.Services
{
    public class CatalogService : Grpc.CatalogService.CatalogServiceBase
    {
        private readonly ILogger<CatalogService> _logger;

        internal static List<Product> _products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Sör", Stock = 10, UnitPrice = 250 },
                new Product { ProductId = 2, Name = "Bor", Stock = 5, UnitPrice = 890 },
                new Product { ProductId = 3, Name = "Csoki", Stock = 15, UnitPrice = 200 },
            };

        public CatalogService(ILogger<CatalogService> logger)
        {
            _logger = logger;
        }

        public override Task<ProductsResponse> GetProducts(Empty request, ServerCallContext context)
        {
            var response = new ProductsResponse();
            response.Products.Add(_products);
            return Task.FromResult(response);
        }

        public override Task<Product> GetProduct(GetProductRequest request, ServerCallContext context)
        {
            var product = _products.SingleOrDefault(p => p.ProductId == request.ProductId);
            return Task.FromResult(product);
        }
    }
}
