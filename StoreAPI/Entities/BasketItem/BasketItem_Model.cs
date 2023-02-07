namespace StoreAPI.Entities
{
    public class BasketItem
    {
        public Guid BasketItemKey { get; set; }
        public string BasketItemId { get; set; } = null!;
        public Guid BasketKey { get; set; }
        public Guid ProductKey { get; set; }
        public int Quantity { get; set; }
        public virtual Basket Basket { get; set; }
        public virtual Product Product { get; set; }
    }
}
