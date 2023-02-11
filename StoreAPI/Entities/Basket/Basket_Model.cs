namespace StoreAPI.Entities
{
    public class Basket
    {
        public Guid UserKey { get; set; }
        public Guid BasketKey { get; set; }
        public virtual ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
