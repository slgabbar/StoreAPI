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
        public async Task<ActionResult<Product>> GetBasket()
        {
            var basket = await _context.Baskets
                .AsNoTracking()
                .Include(x => x.BasketItems)
                .ThenInclude(x => x.Product)
                .Where(x => x.BasketId == Request.Cookies["basketId"])
                .Select(x => new CartViewModel
                {
                    CartId = x.BasketId,
                    Items = x.BasketItems.Select(y => new CartViewModel.Item
                    {
                        Name = y.Product.Name,
                        Quantity = y.Quantity,
                        TotalPrice = y.Quantity * y.Product.Price
                    }).ToList()
                }).FirstOrDefaultAsync();
            
            return basket != null ? Ok(basket) : NotFound();
        }

        [HttpPost("AddToCart")]
        public async Task<ActionResult> AddItemToBasket(Guid productKey, int quantity)
        {
            var basket = await _context.Baskets
                .Include(x => x.BasketItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync();

            if (basket != null)
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
                        BasketItemId = "BI00001",
                        Quantity = quantity,
                        ProductKey = productKey,
                    });
                }
            }
            else
            {
                var newBasket = new Basket
                {
                    BasketId = "B00001"
                };

                newBasket.BasketItems.Add(new BasketItem
                {
                    BasketItemId = "BI00001",
                    Quantity = quantity,
                    ProductKey = productKey,
                });

                _context.Baskets.Add(newBasket);
            }

            var result = await _context.SaveChangesAsync() > 0;
            
            if (result)
                return StatusCode(201);

            return BadRequest("Error creating basket");
        }
        
        [HttpPost("RemoveFromCart")]
        public async Task<ActionResult> RemoveItemFromBasket(Guid productKey, int quantity)
        {
            var basket = await _context.Baskets
                .Include(x => x.BasketItems)
                .ThenInclude(x => x.Product)
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
    }
}