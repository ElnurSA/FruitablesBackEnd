using System;
using FruitablesProject.Data;
using FruitablesProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FruitablesProject.Services
{
    public class SettingServices : ISettingService
	{
		private readonly AppDbContext _context;

		public SettingServices(AppDbContext context)
		{
			_context = context;
		}

		public async Task<Dictionary<string, string>> GetAllAsync()
		{
			
			return await _context.Settings.ToDictionaryAsync(m => m.Key, m => m.Value);
        }

	}
}

