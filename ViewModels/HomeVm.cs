using System;
using FruitablesProject.Models;

namespace FruitablesProject.ViewModels
{
	public class HomeVm
	{
		public List<Slider> Sliders { get; set; }
		public SliderDescriptions SliderDescription { get; set; }
		
		public List<Product> Products { get; set; }
		public List<UnderSliderInformation> UnderSliderInformation { get; set; }
		public List<FruitsAdvertisment> FruitsAdvertisment { get; set; }
        public List<Category> Categories { get; set; }

    }
}

