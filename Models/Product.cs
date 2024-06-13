using System;
namespace FruitablesProject.Models
{
	public class Product : BaseEntity
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public int CategoryId { get; set; }
		public int Count { get; set; }
		public int RatingStars { get; set; }
		public Category Category { get; set; }
		public ICollection<ProductImage> ProductImages { get; set; }
	}
}

