using System;
using FruitablesProject.Data;
using FruitablesProject.Models;
using FruitablesProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FruitablesProject.Services
{
    public class ProductService : IProductSerivce
    {
        private readonly AppDbContext _context;
        
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(m => m.Category).Include(m => m.ProductImages).ToListAsync();
        }

        public async Task<List<Product>> GetAllWithImage(int count)
        {
            return await _context.Products.Include(m => m.ProductImages).Take(count).ToListAsync();
        }
        public async Task<Product> GetByIdAsync(int? id)
        {
            return await _context.Products.Where(m => !m.SoftDeleted)
                                          .Include(m => m.Category)
                                          .Include(m => m.ProductImages)
                                          .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

