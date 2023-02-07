namespace StoreAPI.Entities
{
    public class Basket
    {
        public Guid BasketKey { get; set; }
        public string BasketId { get; set; } = null!;

        public virtual ICollection<BasketItem> BasketItems { get; set; }
    }
}
