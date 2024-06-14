using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FruitablesProject.Data;
using FruitablesProject.Helpers.Extensions;
using FruitablesProject.Models;
using FruitablesProject.ViewModels.Categories;
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
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();

            List<SliderVM> result = sliders.Select(m => new SliderVM { Id = m.Id, Name = m.Name, Image = m.Image }).ToList();
            return View(result);
        }

        
        //Create 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SlidersCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "File must be only image format");
                return View();
            }

            if (!request.Image.CheckFileSize(200))
            {
                ModelState.AddModelError("Image", "Image size must be max 500kb");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;

            string path = Path.Combine(_env.WebRootPath, "img", fileName);

            await request.Image.SaveFileToLocalAsync(path);

            await _context.Sliders.AddAsync(new Slider { Image = fileName, Name = request.Name});

            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));
        }

        //Delete

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var slider = await _context.Sliders.Where(m => m.Id == id).FirstOrDefaultAsync();
            if (slider is null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "img", slider.Image);
            path.DeleteFileFromLocal();


            _context.Sliders.Remove(slider);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //Detail

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var slider = await _context.Sliders.Where(m => m.Id == id).FirstOrDefaultAsync();

            SlidersDetailVM slidersDetailVM = new()
            {
                Image = slider.Image,
                Name = slider.Name
            };
            return View(slidersDetailVM);
        }

        //Edit

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _context.Sliders.Where(m => m.Id == id).FirstOrDefaultAsync();

            if (slider is null) return NotFound();

            return View(new SliderEditVM { Image = slider.Image, Name = slider.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderEditVM request)
        {

           
            if (id is null) return BadRequest();

            var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (slider is null) return NotFound();

            if (request.NewImage is null && request.Name is null) return RedirectToAction(nameof(Index));

       
            if(request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File must be only image format");
                    request.Image = slider.Image;
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(200))
                {
                    ModelState.AddModelError("NewImage", "Image size must be max 500kb");
                    request.Image = slider.Image;
                    return View(request);
                }

                string oldPath = Path.Combine(_env.WebRootPath, "img", slider.Image);

                oldPath.DeleteFileFromLocal();



                string fileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;

                string newPath = Path.Combine(_env.WebRootPath, "img", fileName);

                await request.NewImage.SaveFileToLocalAsync(newPath);

                slider.Image = fileName;
            }
            

            if(request.Name is not null)
            {
                slider.Name = request.Name;
            }



            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

    }
}

