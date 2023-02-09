namespace StoreAPI.ViewModels
{
    public class BasketDto
    {
        public Guid BasketKey { get; set; }
        public List<ItemDto> Items { get; set; } = new List<ItemDto>();
        public long CartPrice => Items.Sum(x => x.TotalPrice);
        public class ItemDto
        {
            public string Name { get; set; } = null!;
            public int Quantity { get; set; }
            public long TotalPrice { get; set; }
        }
    }
}
