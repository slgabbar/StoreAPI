namespace StoreAPI.ViewModels
{
    public class BasketDto
    {
        public Guid UserKey { get; set; }
        public List<ItemDto> Items { get; set; } = new List<ItemDto>();
        public class ItemDto
        {
            public Guid ProductKey { get; set; }
            public string Name { get; set; } = null!;
            public string Type { get; set; } = null!;
            public string Brand { get; set; } = null!;
            public string PictureUrl { get; set; }
            public int Quantity { get; set; }
            public long Price { get; set; }
        }
    }
}
