using System;
namespace FruitablesProject.ViewModels.Basket
{
	public class CartVM
	{
		public List<BasketProductsVM> BasketProducts { get; set; }
		public decimal GrandTotal { get; set; }
	}
}

