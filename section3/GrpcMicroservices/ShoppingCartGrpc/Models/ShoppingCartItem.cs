namespace ShoppingCartGrpc.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
        public float Price { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
