using System;
using FruitablesProject.Models;
using FruitablesProject.ViewModels.Products;

namespace FruitablesProject.Services.Interfaces
{
	public interface IProductSerivce
	{
		Task<List<Product>> GetAllWithImage(int count);
		Task<Product> GetByIdAsync(int? id);
		Task<List<Product>> GetAllAsync();
		List<ProductAdminVM> GetMappedDatas(List<Product> products);
		Task<List<Product>> GetAllPaginateAsync(int pageCount, int take = 4);
		Task<int> GetCountAsync();
		Task CreateAsync(Product product);
		Task DeleteProduct(Product product);

	}
}

