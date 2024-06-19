using System;
using FruitablesProject.Models;
using FruitablesProject.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FruitablesProject.Services.Interfaces
{
	public interface ICategoryService
	{
        Task<List<Category>> GetAllAsyn();
        Task<List<CategoryVM>> GetAllOrderByDesc();
        Task<Category> GetWithProductsAsync(int id);
        Task<bool> ExistAsync(string name);
        Task CreateCategory(CategoryCreateVM category);
        Task DeleteCategory(Category category);
        Task<Category> GetByIdAsync(int id);
        Task EditAsync(Category category, CategoryEditVM categoryEdit);
        Task<SelectList> GetAllBySelectedAsync();
    }
}

