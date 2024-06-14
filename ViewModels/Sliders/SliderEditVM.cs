using System;
using System.ComponentModel.DataAnnotations;

namespace FruitablesProject.ViewModels.Sliders
{
	public class SliderEditVM
	{
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(20, ErrorMessage = "Length must be max 20")]
        public string Name { get; set; }
    }
}

