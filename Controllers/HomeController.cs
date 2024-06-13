using System.Diagnostics;
using FruitablesProject.Data;
using FruitablesProject.Models;
using FruitablesProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitablesProject.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Slider> sliders = await _context.Sliders.ToListAsync();
        SliderDescriptions sliderDescription = await _context.SliderDescription.FirstOrDefaultAsync();
        List<Category> categories = await _context.Categories.ToListAsync();
        List<Product> products = await _context.Products.Include(m=>m.ProductImages).Take(8).ToListAsync();
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

    public async Task<IActionResult> ShowMore()
    {
        List<Product> products = await _context.Products.Include(m => m.ProductImages).ToListAsync();
        return Ok(products);
    }
}

