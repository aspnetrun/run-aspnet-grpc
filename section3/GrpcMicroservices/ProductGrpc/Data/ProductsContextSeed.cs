using ProductGrpc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductGrpc.Data
{
    public class ProductsContextSeed
    {
        public static void SeedAsync(ProductsContext productsContext)
        {
            if (!productsContext.Product.Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        ProductId = 1,
                        Name = "Mi10T",
                        Description = "New Xiaomi Phone Mi10T",
                        Price = 699,
                        Status = ProductGrpc.Models.ProductStatus.INSTOCK,
                        CreatedTime = DateTime.UtcNow
                    },
                    new Product
                    {
                        ProductId = 2,
                        Name = "P40",
                        Description = "New Huawei Phone P40",
                        Price = 899,
                        Status = ProductGrpc.Models.ProductStatus.INSTOCK,
                        CreatedTime = DateTime.UtcNow
                    },
                    new Product
                    {
                        ProductId = 3,
                        Name = "A50",
                        Description = "New Samsung Phone A50",
                        Price = 399,
                        Status = ProductGrpc.Models.ProductStatus.INSTOCK,
                        CreatedTime = DateTime.UtcNow
                    }
                };
                productsContext.Product.AddRange(products);
                productsContext.SaveChanges();
            }
        }
    }
}
