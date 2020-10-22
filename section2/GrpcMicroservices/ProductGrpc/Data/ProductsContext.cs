using Microsoft.EntityFrameworkCore;
using ProductGrpc.Models;
using System;

namespace ProductGrpc.Data
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Mi10T",
                    Description = "New Xiaomi Phone Mi10T",
                    Price = 699,
                    Status = ProductGrpc.Models.ProductStatus.INSTOCK,
                    CreateTime = DateTime.UtcNow
                }
            );
        }
    }
}
