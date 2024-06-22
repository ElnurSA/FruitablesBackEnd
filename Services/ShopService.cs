using System;
using FruitablesProject.Data;
using FruitablesProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FruitablesProject.Services
{
	public class ShopService
	{
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;


        public ShopService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Product>> GetAllPaginateAsync(int pageCount, int take = 8)
        {
            return await _context.Products.Include(m => m.Category).Include(m => m.ProductImages).Skip((pageCount - 1) * take).Take(take).ToListAsync();
        }

    }
}

