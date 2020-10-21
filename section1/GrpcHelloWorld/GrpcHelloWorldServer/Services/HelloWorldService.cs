using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcHelloWorldServer.Services
{
    public class HelloWorldService : HelloService.HelloServiceBase
    {
        private readonly ILogger<HelloWorldService> _logger;

        public HelloWorldService(ILogger<HelloWorldService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloResponse> SayHello(HelloRequest request, ServerCallContext context)
        {
            string resultMessage = $"Hello {request.Name}";

            var response = new HelloResponse
            {
                Message = resultMessage
            };

            return Task.FromResult(response);
        }
    }
}
