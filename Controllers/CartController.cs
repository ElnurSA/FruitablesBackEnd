using System;
using FruitablesProject.Data;
using FruitablesProject.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FruitablesProject.Controllers
{
	public class CartController : Controller
	{
		private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
		public CartController(AppDbContext context, IHttpContextAccessor accessor)
		{
			_context = context;
            _accessor = accessor;

        }

        [HttpGet]
		public async Task<IActionResult> Index()
		{
            List<BasketVM>? basketProducts = null;

            if (_accessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basketProducts = new List<BasketVM>();
            }
            var products = await _context.Products.Include(m => m.Category).Include(m => m.ProductImages).ToListAsync();

            List<BasketProductsVM> basket = new();

            foreach (var item in basketProducts)
            {
                var dbProduct = products.FirstOrDefault(m => m.Id == item.Id);

                basket.Add(new BasketProductsVM
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    Description = dbProduct.Description,
                    Price = dbProduct.Price,
                    Count = item.Count,
                    Category = dbProduct.Category.Name,
                    Image = dbProduct.ProductImages.FirstOrDefault(m=>m.IsMain).Name,
                    Total = item.Count * dbProduct.Price
                });
            }

            CartVM response = new()
            {
                BasketProducts = basket,
                GrandTotal = basketProducts.Sum(m => m.Count * m.Price)
               };

			return View(response);
		}

        [HttpPost]
        public IActionResult DeleteProductFromBasket(int? id)
        {
            if (id is null) return BadRequest();

            List<BasketVM>? basketProducts = new();

            if (_accessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }

            basketProducts = basketProducts.Where(m => m.Id != id).ToList();

            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));

            int count = basketProducts.Sum(m => m.Count);
            decimal total = basketProducts.Sum(m => m.Count * m.Price);
            return Ok(new { count, total });
        }
	}
}

