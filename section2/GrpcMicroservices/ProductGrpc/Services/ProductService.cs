using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ProductGrpc.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductGrpc.Services
{
    public class ProductService : ProductProtoService.ProductProtoServiceBase
    {
        private readonly ProductsContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ProductsContext context, ILogger<ProductService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override Task<Empty> Test(Empty request, ServerCallContext context)
        {
            return base.Test(request, context);
        }

        public override async Task<ProductModel> GetProduct(GetProductRequest request, ServerCallContext context)
        {
            var product = await _context.Product.FindAsync(request.ProductId);
            if(null != product)
            {
                var productModel = new ProductModel
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Status = ProductStatus.Instock,
                    CreatedTime = Timestamp.FromDateTime(product.CreateTime)
                };

                return productModel;
            }
            return null;
        }

        public override Task GetAllProducts(GetAllProductsRequest request, IServerStreamWriter<ProductModel> responseStream, ServerCallContext context)
        {
            return base.GetAllProducts(request, responseStream, context);
        }

    }
}
