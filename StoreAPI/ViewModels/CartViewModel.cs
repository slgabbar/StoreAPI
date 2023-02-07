namespace StoreAPI.ViewModels
{
    public class CartViewModel
    {
        public string CartId { get; set; } = null!;

        public List<Item> Items { get; set; } = new List<Item>();

        public long CartPrice => Items.Sum(x => x.TotalPrice);

        public class Item
        {
            public string Name { get; set; } = null!;
            public int Quantity { get; set; }
            public long TotalPrice { get; set; }
        }
    }
}
