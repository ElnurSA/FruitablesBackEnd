using System;
using FruitablesProject.Models;

namespace FruitablesProject.ViewModels.Products
{
	public class ProductVM
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Count { get; set; }
        public int RatingStars { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public List<Category> AllCategories { get; set; }
        public int ProductCount { get; set; }
        public List<Product> Products { get; set; }
    }
}

