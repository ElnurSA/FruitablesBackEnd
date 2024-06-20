using System;
namespace FruitablesProject.ViewModels.Products
{
	public class ProductEditImageVM
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string Image { get; set; }
		public bool IsMain { get; set; }
	}
}

