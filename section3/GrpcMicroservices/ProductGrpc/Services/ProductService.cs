using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductGrpc.Data;
using ProductGrpc.Models;
using ProductGrpc.Protos;
using System;
using System.Threading.Tasks;

namespace ProductGrpc.Services
{
    public class ProductService : ProductProtoService.ProductProtoServiceBase
    {
        private readonly ProductsContext _productDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ProductsContext productDbContext, IMapper mapper, ILogger<ProductService> logger)
        {
            _productDbContext = productDbContext ?? throw new ArgumentNullException(nameof(productDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }        

        public override Task<Empty> Test(Empty request, ServerCallContext context)
        {
            return base.Test(request, context);
        }

        public override async Task<ProductModel> GetProduct(GetProductRequest request, 
                                                                ServerCallContext context)
        {
            var product = await _productDbContext.Product.FindAsync(request.ProductId);
            if (product == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Product with ID={request.ProductId} is not found."));
            }
            var productModel = _mapper.Map<ProductModel>(product);
            return productModel;                        
        }

        public override async Task GetAllProducts(GetAllProductsRequest request, 
                                                    IServerStreamWriter<ProductModel> responseStream, 
                                                    ServerCallContext context)
        {
            var productList = await _productDbContext.Product.ToListAsync();
            foreach (var product in productList)
            {
                var productModel = _mapper.Map<ProductModel>(product);
                await responseStream.WriteAsync(productModel);
            }
        }

        public override async Task<ProductModel> AddProduct(AddProductRequest request, ServerCallContext context)
        {
            var product = _mapper.Map<Product>(request.Product);

            _productDbContext.Product.Add(product);
            await _productDbContext.SaveChangesAsync();

            _logger.LogInformation("Product successfully added : {productId}_{productName}", product.ProductId, product.Name);

            var productModel = _mapper.Map<ProductModel>(product);
            return productModel;
        }

        public override async Task<ProductModel> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
        {
            var product = _mapper.Map<Product>(request.Product);

            bool isExist = await _productDbContext.Product.AnyAsync(p => p.ProductId == product.ProductId);
            if (!isExist)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Product with ID={product.ProductId} is not found."));
            }

            _productDbContext.Entry(product).State = EntityState.Modified;

            try
            {
                await _productDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            var productModel = _mapper.Map<ProductModel>(product);
            return productModel;
        }

        public override async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
        {
            var product = await _productDbContext.Product.FindAsync(request.ProductId);
            if (product == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Product with ID={request.ProductId} is not found."));
            }

            _productDbContext.Product.Remove(product);
            var deleteCount = await _productDbContext.SaveChangesAsync();

            var response = new DeleteProductResponse
            {
                Success = deleteCount > 0
            };

            return response;
        }

        public override async Task<InsertBulkProductResponse> InsertBulkProduct(IAsyncStreamReader<ProductModel> requestStream, ServerCallContext context)
        {
            // https://csharp.hotexamples.com/examples/-/IAsyncStreamReader/-/php-iasyncstreamreader-class-examples.html

            while (await requestStream.MoveNext())
            {
                var product = _mapper.Map<Product>(requestStream.Current);
                _productDbContext.Product.Add(product);
            }

            var insertCount = await _productDbContext.SaveChangesAsync();

            var response = new InsertBulkProductResponse
            {
                Success = insertCount > 0,
                InsertCount = insertCount
            };

            return response;
        }

    }
}
