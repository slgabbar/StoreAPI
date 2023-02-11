using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Entities;
using StoreAPI.ViewModels;

namespace StoreAPI.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly StoreDbContext _context;
        private readonly Guid _userKey;

        public BasketController(StoreDbContext context)
        {
            _context = context;
            _userKey = new Guid("1b6ae95c-b028-46ae-9555-71a8458afa2a");
        }


        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var userKey = GetKeyFromRequset(Request, "userKey");
            
            if (userKey == null)
                return NotFound();

            var basket = await ReadBasketByUserAsync(userKey.Value);

            return basket != null ? Ok(MapToBasketDto(basket)) : NotFound();
        }

        [HttpPost("AddToCart")]
        public async Task<ActionResult> AddItemToBasket(Guid productKey, int quantity)
        {
            Basket? basket = null;
            
            var userKey = GetKeyFromRequset(Request, "userKey");
            if (userKey != null)
                basket = await ReadBasketByUserAsync(userKey.Value);

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
                return CreatedAtRoute("GetBasket", MapToBasketDto(basket));

            return BadRequest("Error creating basket");
        }
        
        private Basket GetBasketFromProduct(Guid productKey, int quantity)
        {
            var cookieOptions = new CookieOptions
            {
                IsEssential= true,
                Expires= DateTimeOffset.UtcNow.AddDays(30),
            };

            Response.Cookies.Append("userKey", _userKey.ToString(), cookieOptions);

            return new Basket
            {
                UserKey = _userKey,
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

        private async Task<Basket?> ReadBasketByUserAsync(Guid userKey) => 
            await _context.Baskets
                .Include(x => x.BasketItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.UserKey == userKey);

        private BasketDto MapToBasketDto(Basket basket)
        {
            return new BasketDto
            {
                BasketKey = basket.BasketKey,
                Items = basket.BasketItems.Select(y => new BasketDto.ItemDto
                {
                    Quantity = y.Quantity,
                }).ToList()
            };
        }
    }
}