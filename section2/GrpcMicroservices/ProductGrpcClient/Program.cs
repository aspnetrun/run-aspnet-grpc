using Grpc.Net.Client;
using ProductGrpc;
using System;
using System.Threading.Tasks;

namespace ProductGrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new ProductProtoService.ProductProtoServiceClient(channel);

            var response = await client.GetProductAsync(
                                new GetProductRequest
                                {
                                    ProductId = 1
                                });
            
            Console.WriteLine("Greeting: " + response.ToString());
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
