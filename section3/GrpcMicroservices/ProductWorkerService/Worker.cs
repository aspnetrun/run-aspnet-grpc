using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductGrpc.Protos;

namespace ProductWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;        

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;            
        }       

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    using var channel = GrpcChannel.ForAddress(_config.GetValue<string>("WorkerService:ServerUrl"));
                    var client = new ProductProtoService.ProductProtoServiceClient(channel);

                    // AddProductAsync
                    Console.WriteLine("AddProductAsync started...");
                    var addProductResponse = await client.AddProductAsync(
                                        new AddProductRequest
                                        {
                                            Product = new ProductModel
                                            {
                                                Name = _config.GetValue<string>("WorkerService:ProductName") + DateTimeOffset.Now,
                                                Description = "New Red Phone Mi10T",
                                                Price = 699,
                                                Status = ProductStatus.Instock,
                                                CreatedTime = Timestamp.FromDateTime(DateTime.UtcNow)
                                            }
                                        });

                    Console.WriteLine("AddProduct Response: " + addProductResponse.ToString());
                    Thread.Sleep(1000);

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }       

                await Task.Delay(_config.GetValue<int>("WorkerService:TaskInterval"), stoppingToken);
            }
        }
    }
}
