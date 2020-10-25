using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private ProductProtoService.ProductProtoServiceClient _client = null;

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;            
        }

        public ProductProtoService.ProductProtoServiceClient Client 
        { 
            get
            {
                if (null == _client)
                {
                    using var channel = GrpcChannel.ForAddress(_config.GetValue<string>("WorkerService:ServerUrl"));
                    _client = new ProductProtoService.ProductProtoServiceClient(channel);
                }

                return _client;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Thread.Sleep(2000);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    Console.WriteLine("GetProductAsync started...");
                    var response = await Client.GetProductAsync(
                                        new GetProductRequest
                                        {
                                            ProductId = 1
                                        });

                    Console.WriteLine("GetProductAsync Response: " + response.ToString());
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
