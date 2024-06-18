using System;
using FruitablesProject.Models;

namespace FruitablesProject.ViewModels.Basket
{
	public class BasketProductsVM
	{
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
        public int RatingStars { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public decimal Total { get; set; }

    }
}

