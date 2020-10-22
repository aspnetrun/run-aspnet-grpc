using System;

namespace ProductGrpc.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime CreatedTime { get; set; }
    }

    public enum ProductStatus
    {
        INSTOCK = 0,
        LOW = 1,
        NONE = 2
    }
}

