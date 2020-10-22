using System;

namespace ProductGrpc.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public enum ProductStatus
    {
        INSTOCK = 0,
        LOW = 1,
        NONE = 2
    }
}

