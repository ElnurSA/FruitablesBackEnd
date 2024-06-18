using System;
using FruitablesProject.Models;

namespace FruitablesProject.Services.Interfaces
{
	public interface IProductSerivce
	{
		Task<List<Product>> GetAllWithImage(int count);
		Task<Product> GetByIdAsync(int? id);
		Task<List<Product>> GetAllAsync();

	}
}

