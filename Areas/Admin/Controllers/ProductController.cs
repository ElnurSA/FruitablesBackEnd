using System;
using FruitablesProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FruitablesProject.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IProductSerivce _productSerivce;
		public ProductController(IProductSerivce productSerivce)
		{
            _productSerivce = productSerivce;

        }
		public async Task<IActionResult> Index()
		{
			return View();
		}
	}
}

