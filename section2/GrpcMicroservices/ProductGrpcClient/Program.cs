using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using ProductGrpc.Protos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductGrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // wait for server is running
            Console.WriteLine("Waiting for server is running");
            Thread.Sleep(2000);

            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new ProductProtoService.ProductProtoServiceClient(channel);
            
            // GetProductAsync
            Console.WriteLine("GetProductAsync started...");
            var response = await client.GetProductAsync(
                                new GetProductRequest
                                {
                                    ProductId = 1
                                });
            
            Console.WriteLine("GetProductAsync Response: " + response.ToString());
            Thread.Sleep(1000);

            // GetAllProducts
            Console.WriteLine("GetAllProducts started...");
            using (var clientData = client.GetAllProducts(new GetAllProductsRequest()))
            {
                while (await clientData.ResponseStream.MoveNext(new System.Threading.CancellationToken()))
                {
                    var currentProduct = clientData.ResponseStream.Current;
                    Console.WriteLine(currentProduct);
                }
            }
            Thread.Sleep(1000);

            // GetAllProducts with C# 8
            Console.WriteLine("GetAllProducts with C#8 started...");                        
            using var clientData2 = client.GetAllProducts(new GetAllProductsRequest());
            await foreach (var responseData in clientData2.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine(responseData);
            }
            Thread.Sleep(1000);

            // AddProductAsync
            Console.WriteLine("AddProductAsync started...");
            var addProductResponse = await client.AddProductAsync(
                                new AddProductRequest
                                {
                                    Product = new ProductModel
                                    {
                                        Name = "Red",
                                        Description = "New Red Phone Mi10T",
                                        Price = 699,
                                        Status = ProductStatus.Instock,
                                        CreatedTime = Timestamp.FromDateTime(DateTime.UtcNow)
                                    }
                                });

            Console.WriteLine("AddProduct Response: " + addProductResponse.ToString());
            Thread.Sleep(1000);

            Console.WriteLine("UpdateProductAsync started...");
            var updateProductResponse = await client.UpdateProductAsync(
                                 new UpdateProductRequest
                                 {
                                     Product = new ProductModel
                                     {
                                         ProductId = 1,
                                         Name = "Red",
                                         Description = "New Red Phone Mi10T",
                                         Price = 699,
                                         Status = ProductStatus.Instock,
                                         CreatedTime = Timestamp.FromDateTime(DateTime.UtcNow)
                                     }
                                 });

            Console.WriteLine("UpdateProductAsync Response: " + updateProductResponse.ToString());
            Thread.Sleep(1000);

            Console.WriteLine("DeleteProductAsync started...");
            var deleteProductResponse = await client.DeleteProductAsync(
                                 new DeleteProductRequest
                                 {
                                     ProductId = 3                                     
                                 });

            Console.WriteLine("DeleteProductAsync Response: " + deleteProductResponse.Success.ToString());
            Thread.Sleep(1000);

            // GetAllProducts with C# 8
            Console.WriteLine("GetAllProducts with C#8 started...");            
            using var clientData3 = client.GetAllProducts(new GetAllProductsRequest());
            await foreach (var responseData in clientData3.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine(responseData);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
