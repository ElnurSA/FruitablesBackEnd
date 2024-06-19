using System;
using FruitablesProject.Models;

namespace FruitablesProject.ViewModels.Products
{
	public class ProductAdminVM
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Count { get; set; }
        public int RatingStars { get; set; }
        public string Category { get; set; }
        public string ProductImages { get; set; }
    }
}

