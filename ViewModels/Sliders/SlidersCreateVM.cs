using System;
using System.ComponentModel.DataAnnotations;

namespace FruitablesProject.ViewModels.Sliders
{
	public class SlidersCreateVM
	{

		public IFormFile Image { get; set; }
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(20, ErrorMessage = "Length must be max 20")]
        public string Name { get; set; }
	}
}

