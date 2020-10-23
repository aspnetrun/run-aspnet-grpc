using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductGrpc.Data;

namespace ProductGrpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            SeedDatabase(host);
            host.Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SeedDatabase(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var moviesContext = services.GetRequiredService<ProductsContext>();
            ProductsContextSeed.SeedAsync(moviesContext);
        }
    }
}
