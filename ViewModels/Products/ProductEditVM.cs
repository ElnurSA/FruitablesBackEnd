using System;
using System.ComponentModel.DataAnnotations;

namespace FruitablesProject.ViewModels.Products
{
	public class ProductEditVM
	{
        public List<ProductEditImageVM> ExistImages { get; set; } = new List<ProductEditImageVM>();
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> NewImages { get; set; } = new List<IFormFile>();
    }
}

