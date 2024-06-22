using System;
using System.Data;
using FruitablesProject.Data;
using FruitablesProject.Helpers;
using FruitablesProject.Models;
using FruitablesProject.Services.Interfaces;
using FruitablesProject.ViewModels;
using FruitablesProject.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FruitablesProject.Controllers
{
	public class ShopController : Controller
	{
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductSerivce _productSerivce;
        private readonly ICategoryService _categoryService;

        public ShopController(AppDbContext context, IHttpContextAccessor accessor, IProductSerivce productSerivce, ICategoryService categoryService)
        {
            _context = context;
            _accessor = accessor;
            _productSerivce = productSerivce;
            _categoryService = categoryService;
        }
        [HttpGet]
		public async Task<IActionResult> Index()
		{
           
           
            List<Category> categories = await _categoryService.GetAllAsyn();
            List<Product> products = await _context.Products.Include(m => m.ProductImages).ToListAsync();

            ShopVM model = new()
            {
                Categories = categories,
                Products = products,
            };
            return View(model);

        }

        
    }
}

