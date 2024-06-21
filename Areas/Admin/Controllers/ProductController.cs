using System;
using FruitablesProject.Helpers;
using FruitablesProject.Helpers.Enums;
using FruitablesProject.Helpers.Extensions;
using FruitablesProject.Helpers.Requests;
using FruitablesProject.Models;
using FruitablesProject.Services.Interfaces;
using FruitablesProject.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FruitablesProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class ProductController : Controller
	{
		private readonly IProductSerivce _productSerivce;
		private readonly ICategoryService _categoryService;
		private readonly IWebHostEnvironment _env; 

		public ProductController(IProductSerivce productSerivce, ICategoryService categoryService, IWebHostEnvironment env)
		{
            _productSerivce = productSerivce;
			_categoryService = categoryService;
			_env = env;

        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet]
		public async Task<IActionResult> Index(int page = 1 )
		{
			var paginateData = await _productSerivce.GetAllPaginateAsync(page);
			var mappedData = _productSerivce.GetMappedDatas(paginateData);



			int pageCount = await GetPageCountAysnc(4);

			Paginate<ProductAdminVM> model = new(mappedData, pageCount, page);

			return View(model);
		}

		private async Task<int> GetPageCountAysnc(int take)
		{
			int count = await _productSerivce.GetCountAsync();
			return (int)Math.Ceiling((decimal)count / take);
        }


		//Detail
		[HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
			if (id is null) return BadRequest();

			Product product = await _productSerivce.GetByIdAsync(id);

			if (product is null) return NotFound();

			List<ProductImageVM> productImages = new();

			foreach (var item in product.ProductImages)
			{
				productImages.Add(new ProductImageVM
				{
					Image = item.Name,
					IsMain = item.IsMain
				});
			}

			ProductDetailVM model = new()
			{
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				RatingStars = product.RatingStars,
				Category = product.Category.Name,
				Image = productImages

			};

			return View(model);
        }

        [Authorize(Roles = "SuperAdmin")]
        //Create
        [HttpGet]
		public async Task<IActionResult> Create()
		{
			ViewBag.Categories = await _categoryService.GetAllBySelectedAsync();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductCreateVM request)
		{
			ViewBag.Categories = await _categoryService.GetAllBySelectedAsync();
			if (!ModelState.IsValid)
			{
				return View();
			}

			foreach (var item in request.Images)
			{
				if (!item.CheckFileSize(500))
				{
					ModelState.AddModelError("Image", "Image size must be less than 500kg");
					return View();
				}
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "File type is wrong");
                    return View();
                }
            }

			List<ProductImage> images = new();

			foreach (var item in request.Images)
			{
				string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

				string path = Path.Combine(_env.WebRootPath, "img", fileName);

				await item.SaveFileToLocalAsync(path);

				images.Add(new ProductImage
				{
					Name = fileName
				});
			}

			images.FirstOrDefault().IsMain = true;

			Product product = new()
			{
				Name = request.Name,
				Description = request.Description,
				Price = request.Price,
				CategoryId = request.CategoryId,
				ProductImages = images
			};

			await _productSerivce.CreateAsync(product);
            
            return RedirectToAction(nameof(Index));
		}

		//Delete
		[HttpPost]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id is null) return BadRequest();

			Product product = await _productSerivce.GetByIdAsync(id);

			if (product is null) return NotFound();

			foreach (var item in product.ProductImages)
			{
				string path = Path.Combine(_env.WebRootPath, "img", item.Name);

				path.DeleteFileFromLocal();
			}

			await _productSerivce.DeleteProduct(product);

			return RedirectToAction(nameof(Index));
		}

		//Edit
		[HttpPost]
		public async Task<IActionResult> DeleteProductImage(DeleteProductImageRequest request)
		{
			await _productSerivce.DeleteProductImageAsync(request);
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
            ViewBag.Categories = await _categoryService.GetAllBySelectedAsync();
            if (id is null) return BadRequest();

			Product product = await _productSerivce.GetByIdAsync((int)id);

			if (product is null) return NotFound();

			ProductEditVM response = new()
			{
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				CategoryId = product.CategoryId,
				ExistImages = product.ProductImages.Select(m => new ProductEditImageVM
				{
					Id = m.Id,
					Image = m.Name,
					ProductId = m.ProductId,
					IsMain = m.IsMain
				}).ToList()

			};

			

			return View(response);
		}


        [HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ProductEditVM request)
        {
            ViewBag.Categories = await _categoryService.GetAllBySelectedAsync();
            if (id is null) return BadRequest();

            Product product = await _productSerivce.GetByIdAsync((int)id);

            if (product is null) return NotFound();

			if (!ModelState.IsValid)
			{
				request.ExistImages = product.ProductImages.Select(m => new ProductEditImageVM
				{
					Id = m.Id,
					Image = m.Name,
					ProductId = m.ProductId,
					IsMain = m.IsMain
				}).ToList();
				return View(request);

            }

			if (request.NewImages is not null)
			{
                foreach (var item in request.NewImages)
                {
                    if (!item.CheckFileSize(500))
                    {
                        request.ExistImages = product.ProductImages.Select(m => new ProductEditImageVM
                        {
                            Id = m.Id,
                            Image = m.Name,
                            ProductId = m.ProductId,
                            IsMain = m.IsMain
                        }).ToList();
                        ModelState.AddModelError("NewImage", "Image size must be less than 500kg");
                        return View(request);
                    }
                    if (!item.CheckFileType("image/"))
                    {
                        request.ExistImages = product.ProductImages.Select(m => new ProductEditImageVM
                        {
                            Id = m.Id,
                            Image = m.Name,
                            ProductId = m.ProductId,
                            IsMain = m.IsMain
                        }).ToList();
                        ModelState.AddModelError("NewImage", "File type is wrong");
                        return View(request);
                    }
                }
            }

			await _productSerivce.EditAsync(product, request);
            return RedirectToAction(nameof(Index));
        }



    }
}

