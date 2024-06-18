using System.Diagnostics;
using FruitablesProject.Data;
using FruitablesProject.Models;
using FruitablesProject.Services.Interfaces;
using FruitablesProject.ViewModels;
using FruitablesProject.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FruitablesProject.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _accessor;
    private readonly IProductSerivce _productSerivce;
    private readonly ICategoryService _categoryService;

    public HomeController(AppDbContext context, IHttpContextAccessor accessor, IProductSerivce productSerivce, ICategoryService categoryService)
    {
        _context = context;
        _accessor = accessor;
        _productSerivce = productSerivce;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        List<Slider> sliders = await _context.Sliders.ToListAsync();
        SliderDescriptions sliderDescription = await _context.SliderDescription.FirstOrDefaultAsync();
        List<Category> categories = await _categoryService.GetAllAsyn();
        List<Product> products = await _productSerivce.GetAllWithImage(8);
        List<UnderSliderInformation> underSliderInformation = await _context.UnderSliderInformation.ToListAsync();
        List<FruitsAdvertisment> fruitsAdvertisments = await _context.FruitsAdvertisments.ToListAsync();


        HomeVm model = new()
        {
            Sliders = sliders,
            SliderDescription = sliderDescription,
            Categories = categories,
            Products = products,
            UnderSliderInformation = underSliderInformation,
            FruitsAdvertisment = fruitsAdvertisments
        };
        return View(model);
    }

    //Basket

    [HttpPost]

    public async Task<IActionResult> AddProductToBasket(int? id)
    {
        if (id is null) return BadRequest();

        List<BasketVM>? basketProducts = null;

        if (_accessor.HttpContext.Request.Cookies["basket"] is not null)
        {
            basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
        }
        else
        {
            basketProducts = new List<BasketVM>();
        }

        var dbProduct = await _context.Products.FirstOrDefaultAsync(m => m.Id == (int)id);

        var existData = basketProducts.FirstOrDefault(m => m.Id == (int)id);

        if (existData is not null)
        {
            existData.Count++;
        }
        else
        {
            basketProducts.Add(new BasketVM
            {
                Id = (int)id,
                Count = 1,
                Price = dbProduct.Price
            });
        }




        _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));

        int count = basketProducts.Sum(m => m.Count);

        decimal total = basketProducts.Sum(m => m.Count * m.Price);
        return Ok(new { count, total });

    }



}

