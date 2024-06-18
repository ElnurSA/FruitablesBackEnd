using System.Diagnostics;
using FruitablesProject.Data;
using FruitablesProject.Models;
using FruitablesProject.Services.Interfaces;
using FruitablesProject.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitablesProject.Controllers
{
	public class ProductController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IProductSerivce _productSerivce;

		public ProductController(AppDbContext context, IProductSerivce productSerivce)
		{
			_context = context;
			_productSerivce = productSerivce;
		}

		public async Task<IActionResult> Index(int? id)
		{

			if (id is null) return BadRequest();


			List<Category> allCategories = await _context.Categories.ToListAsync();


			Product existproduct = await _productSerivce.GetByIdAsync(id);
			ProductVM? product = new()
			{
				ProductImages = existproduct.ProductImages,
				Price = existproduct.Price,
				Name = existproduct.Name,
				Description = existproduct.Description,
				Category = existproduct.Category,
				RatingStars = existproduct.RatingStars,
				Count = existproduct.Count,
				AllCategories = allCategories,
				

            };



            return View(product);
        }
	}
}

