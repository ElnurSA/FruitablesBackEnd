using System;
using FruitablesProject.Data;
using FruitablesProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitablesProject.ViewComponents
{
	public class SliderViewComponent : ViewComponent
	{
		private readonly AppDbContext _context;
		public SliderViewComponent(AppDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var sliderData = new SliderVMVC
			{
				Sliders = await _context.Sliders.ToListAsync(),
				SliderDescriptions = await _context.SliderDescription.FirstOrDefaultAsync()
			};

			return await Task.FromResult(View(sliderData));
		}
	}

	public class SliderVMVC
	{
		public List<Slider> Sliders { get; set; }
		public SliderDescriptions SliderDescriptions { get; set; }
	}
}

