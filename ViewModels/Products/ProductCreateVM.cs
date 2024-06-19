using System;
using System.ComponentModel.DataAnnotations;

namespace FruitablesProject.ViewModels.Products
{
	public class ProductCreateVM
	{
		[Required]
		public string Name { get; set; }
		
		[Required]
		public decimal Price { get; set; }
		[Required]
		public string Description { get; set; }
		public int CategoryId { get; set; }
		[Required]
		public List<IFormFile> Images { get; set; }
	}
}

