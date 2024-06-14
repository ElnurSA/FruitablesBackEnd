using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitablesProject.Data;
using FruitablesProject.Models;
using FruitablesProject.ViewModels.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FruitablesProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        public SliderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            List<SliderVM> result = sliders.Select(m => new SliderVM { Id = m.Id, Name = m.Image }).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}

