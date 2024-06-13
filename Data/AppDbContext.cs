using System;
using FruitablesProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FruitablesProject.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderDescriptions> SliderDescription { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<UnderSliderInformation> UnderSliderInformation { get; set; }
        public DbSet<FruitsAdvertisment> FruitsAdvertisments { get; set; }


    }
}

