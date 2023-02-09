using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Entities;
using StoreAPI.ViewModels;

namespace StoreAPI.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreDbContext _context;

        public BasketController(StoreDbContext context)
        {
            _context = context;
        }


        [HttpGet()]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basketKey = GetKeyFromRequset(Request, "basketKey");
            
            if (basketKey == null)
                return NotFound();

            var basket = await ReadBasketAsync(basketKey.Value);

            return basket != null ? Ok(MapToBasketDto(basket)) : NotFound();
        }

        [HttpPost("AddToCart")]
        public async Task<ActionResult> AddItemToBasket(Guid productKey, int quantity)
        {
            var basketKey = GetKeyFromRequset(Request, "basketKey");
            if (basketKey == null)
                return NotFound();

            var basket = await ReadBasketAsync(basketKey.Value);

            if (basket == null)
            {
                basket = GetBasketFromProduct(productKey, quantity);
                _context.Baskets.Add(basket);
            }
            else
            {
                var item = basket.BasketItems.FirstOrDefault(x => x.ProductKey == productKey);
                if (item != null)
                {
                    item.Quantity += quantity;
                }
                else
                {
                    _context.BasketItems.Add(new BasketItem
                    {
                        BasketKey = basket.BasketKey,
                        Quantity = quantity,
                        ProductKey = productKey,
                    });
                }
            }

            var result = await _context.SaveChangesAsync() > 0;

            if (result)
                return CreatedAtRoute(nameof(GetBasket), MapToBasketDto(basket));

            return BadRequest("Error creating basket");
        }
        
        private Basket GetBasketFromProduct(Guid productKey, int quantity)
        {
            return new Basket
            {
                BasketItems = new List<BasketItem>
                {
                    new BasketItem
                    {
                        ProductKey = productKey,
                        Quantity = quantity,
                    }
                }
            };
        }


        [HttpPost("RemoveFromCart")]
        public async Task<ActionResult> RemoveItemFromBasket(Guid productKey, int quantity)
        {
            var basketKey = GetKeyFromRequset(Request, "basketKey");
            if (basketKey == null)
                return NotFound();

            var basket = await _context.Baskets
                .Include(x => x.BasketItems)
                .ThenInclude(x => x.Product)
                .Where(x => x.BasketKey == basketKey)
                .FirstOrDefaultAsync();
            
            if (basket == null)
                return BadRequest("Basket not found");

            var basketItem = basket.BasketItems.FirstOrDefault(x => x.ProductKey == productKey);

            if (basketItem == null)
                return BadRequest("Basket Item does not exist");

            if (basketItem.Quantity > quantity)
                basketItem.Quantity -= quantity;
            else
                _context.BasketItems.Remove(basketItem);

            var result = await _context.SaveChangesAsync() > 0;

            if (result)
                return StatusCode(201);

            return BadRequest("Error creating basket");
        }
        
        private Guid? GetKeyFromRequset(HttpRequest request, string cookie) =>
            Request.Cookies[cookie] != null && Guid.TryParse(Request.Cookies[cookie], out Guid basketKey)
                ? basketKey
                : null;

        private async Task<Basket?> ReadBasketAsync(Guid basketKey) => 
            await _context.Baskets
                .AsNoTracking()
                .Include(x => x.BasketItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.BasketKey == basketKey);

        private BasketDto MapToBasketDto(Basket basket)
        {
            return new BasketDto
            {
                BasketKey = basket.BasketKey,
                Items = basket.BasketItems.Select(y => new BasketDto.ItemDto
                {
                    Name = y.Product.Name,
                    Quantity = y.Quantity,
                    TotalPrice = y.Quantity * y.Product.Price
                }).ToList()
            };
        }
    }
}