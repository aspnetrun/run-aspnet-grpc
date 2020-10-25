using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProductGrpc.Protos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductWorkerService
{
    public class ProductFactory
    {
        private readonly ILogger<ProductFactory> _logger;
        private readonly IConfiguration _config;

        public ProductFactory(ILogger<ProductFactory> logger, IConfiguration config)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public Task<AddProductRequest> Generate()
        {
            var productName = $"{_config.GetValue<string>("WorkerService:ProductName")}_{DateTimeOffset.Now}";
            var productRequest = new AddProductRequest
            {
                Product = new ProductModel
                {
                    Name = productName,
                    Description = $"{productName}_Description",
                    Price = new Random().Next(1000),
                    Status = ProductStatus.Instock,
                    CreatedTime = Timestamp.FromDateTime(DateTime.UtcNow)
                }
            };

            return Task.FromResult(productRequest);
        }
    }
}
