using System;
namespace FruitablesProject.ViewModels.Products
{
	public class ProductDetailVM
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public string Category { get; set; }
		public int RatingStars { get; set; }
		public List<ProductImageVM> Image { get; set; }
	}
}

