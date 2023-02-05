namespace StoreAPI.Entities
{
    public class Product
    {
        public Guid ProductKey { get; set; }
        public string ProductId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public long Price { get; set; }
        public string? PictureUrl { get; set; }
        public string Type { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public int QuantityInStock { get; set; }
    }
}
