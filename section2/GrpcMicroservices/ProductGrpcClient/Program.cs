using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using ProductProtoGrpc;
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


            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
