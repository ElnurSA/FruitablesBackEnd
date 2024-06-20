using System;
using FruitablesProject.Data;
using FruitablesProject.Helpers.Extensions;
using FruitablesProject.Helpers.Requests;
using FruitablesProject.Models;
using FruitablesProject.Services.Interfaces;
using FruitablesProject.ViewModels.Products;
using Microsoft.EntityFrameworkCore;

namespace FruitablesProject.Services
{
    public class ProductService : IProductSerivce
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        
        
        public ProductService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.Include(m => m.Category).Include(m => m.ProductImages).ToListAsync();
        }

        public async Task<List<Product>> GetAllPaginateAsync(int pageCount, int take = 4)
        {
            return await _context.Products.Include(m => m.Category).Include(m => m.ProductImages).Skip((pageCount - 1) * take).Take(take).ToListAsync();
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

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public List<ProductAdminVM> GetMappedDatas(List<Product> products)
        {
            return products.Select(m => new ProductAdminVM
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                Category = m.Category.Name,
                ProductImages = m.ProductImages.FirstOrDefault(m=>m.IsMain).Name
            }).ToList();
        }

        public async Task CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductImageAsync(DeleteProductImageRequest request)
        {
            var product = await _context.Products.Where(m => m.Id == request.ProductId).Include(m => m.ProductImages).FirstOrDefaultAsync();

            var image = product.ProductImages.FirstOrDefault(m => m.Id == request.ImageId);

            string path = _env.GenerateFilePath("img", image.Name);

            path.DeleteFileFromLocal();

            product.ProductImages.Remove(image);

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Product product, ProductEditVM edittedProduct)
        {
            if(edittedProduct.NewImages != null)
            {
                foreach (var item in edittedProduct.NewImages)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;
                    string path = _env.GenerateFilePath("img", fileName);
                    await item.SaveFileToLocalAsync(path);
                    product.ProductImages.Add(new ProductImage { Name = fileName });
                }
            }

            product.Name = edittedProduct.Name;
            product.Description = edittedProduct.Description;
            product.CategoryId = edittedProduct.CategoryId;
            product.Price = edittedProduct.Price;

            await _context.SaveChangesAsync();
            
        }
    }
}

