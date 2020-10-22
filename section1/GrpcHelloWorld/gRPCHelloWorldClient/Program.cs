using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace GrpcHelloWorldClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new HelloService.HelloServiceClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "Hello SwN Client" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
